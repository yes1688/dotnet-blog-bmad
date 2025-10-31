# 開發問題報告

**專案名稱**: dotnet-blog-bmad
**報告日期**: 2025-10-31
**開發階段**: Stories 1.1-4.1 完成後的問題修復階段

---

## 執行摘要

本報告記錄了在完成 BMAD 計劃階段（Stories 1.1-4.1）實作後，實際測試與部署過程中遇到的技術問題。這些問題在計劃階段無法預見，是實作細節和環境配置層面的挑戰。

**問題統計**:
- 總計發現問題: 7 個
- 已修復: 7 個
- 待修復: 0 個

---

## 問題清單

### 問題 #1: OAuth 回調頁面 404 錯誤

**嚴重程度**: 🔴 Critical
**發現時間**: 2025-10-31 (OAuth 登入測試時)
**問題類型**: 缺失檔案

#### 問題描述
用戶完成 Google OAuth 驗證後，系統重導向到 `/Admin/Callback` 時出現 HTTP 404 錯誤。

#### 根本原因
- 只建立了 `Callback.cshtml.cs` (PageModel)
- 缺少對應的 `Callback.cshtml` (View) 檔案
- ASP.NET Core Razor Pages 要求兩者都必須存在

#### 錯誤訊息
```
找不到 localhost 網頁
找不到此網址的網頁：http://localhost:5000/Admin/Callback
HTTP ERROR 404
```

#### 解決方案
建立 `Pages/Admin/Callback.cshtml` 檔案：
```html
@page
@model dotnet_blog_bmad.Pages.Admin.CallbackModel
@{
    Layout = null;
}
<!DOCTYPE html>
<html>
<head>
    <title>Processing...</title>
</head>
<body>
    <p>Processing authentication...</p>
</body>
</html>
```

#### 修復檔案
- `Pages/Admin/Callback.cshtml` (新建)

#### 預防措施
- [ ] 建立自動化測試檢查每個 PageModel 都有對應的 View 檔案
- [ ] 在 Definition of Done 中加入「視圖檔案完整性檢查」

---

### 問題 #2: 登出頁面 404 錯誤

**嚴重程度**: 🔴 Critical
**發現時間**: 2025-10-31 (登入後測試登出功能)
**問題類型**: 缺失檔案

#### 問題描述
用戶點擊登出按鈕後，系統重導向到 `/Admin/Logout` 時出現 HTTP 404 錯誤。

#### 根本原因
- 只建立了 `Logout.cshtml.cs` (PageModel)
- 缺少對應的 `Logout.cshtml` (View) 檔案

#### 錯誤訊息
```
找不到 localhost 網頁
找不到此網址的網頁：http://localhost:5000/Admin/Logout
HTTP ERROR 404
```

#### 解決方案
建立 `Pages/Admin/Logout.cshtml` 檔案：
```html
@page
@model dotnet_blog_bmad.Pages.Admin.LogoutModel
@{
    Layout = null;
}
<!DOCTYPE html>
<html>
<head>
    <title>Logging out...</title>
</head>
<body>
    <p>Logging out...</p>
</body>
</html>
```

#### 修復檔案
- `Pages/Admin/Logout.cshtml` (新建)

---

### 問題 #3: 登出功能 500 錯誤 - Google OAuth SignOut

**嚴重程度**: 🔴 Critical
**發現時間**: 2025-10-31 (建立 Logout.cshtml 並重新部署後)
**問題類型**: 邏輯錯誤

#### 問題描述
建立 Logout.cshtml 後，訪問登出頁面出現 HTTP 500 內部伺服器錯誤。

#### 根本原因
`Logout.cshtml.cs` 中嘗試對 Google OAuth scheme 執行 `SignOutAsync()`，但 Google OAuth 提供者不支援此操作。

#### 錯誤訊息
```
System.InvalidOperationException: The registered sign-out schemes are: 'Cookies',
but 'Google' was requested.
GoogleHandler cannot be used for SignOutAsync.
```

#### 原始代碼
```csharp
public async Task<IActionResult> OnGet()
{
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    await HttpContext.SignOutAsync("Google"); // ❌ 錯誤：Google 不支援
    return Redirect("/");
}
```

#### 解決方案
移除 Google scheme 的 SignOut，只登出 Cookie：
```csharp
public async Task<IActionResult> OnGet()
{
    // 登出 Cookie 認證（這會清除本地認證狀態）
    // 注意：Google OAuth 不支持 SignOutAsync，只需登出 Cookie 即可
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Redirect("/");
}
```

#### 修復檔案
- `Pages/Admin/Logout.cshtml.cs:17-24`

#### 知識點
- OAuth 2.0 提供者（如 Google）只負責身份驗證，不管理 session
- 本地 session 由 Cookie Authentication 管理
- 登出時只需要清除本地 Cookie 認證即可

---

### 問題 #4: jQuery 未定義錯誤

**嚴重程度**: 🟡 High
**發現時間**: 2025-10-31 (瀏覽器控制台)
**問題類型**: 依賴順序錯誤

#### 問題描述
瀏覽器控制台出現 jQuery 相關錯誤：
```
jquery.validate.min.js:4 Uncaught ReferenceError: jQuery is not defined
jquery.validate.unobtrusive.min.js:8 Uncaught ReferenceError: jQuery is not defined
```

#### 根本原因
`_ValidationScriptsPartial.cshtml` 載入了 jQuery Validation 和 jQuery Unobtrusive Validation，但沒有先載入 jQuery 本身。

#### 原始代碼
```html
<script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script>
<script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
```

#### 解決方案
在驗證腳本之前先載入 jQuery：
```html
<!-- jQuery (required for validation scripts) -->
<script src="~/lib/jquery/dist/jquery.min.js"></script>
<!-- jQuery Validation -->
<script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script>
<script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
```

#### 修復檔案
- `Pages/Shared/_ValidationScriptsPartial.cshtml:1-5`

#### 預防措施
- [ ] 建立前端依賴順序檢查清單
- [ ] 在 Definition of Done 中加入「瀏覽器控制台無錯誤」檢查

---

### 問題 #5: EasyMDE Content 欄位驗證衝突

**嚴重程度**: 🟡 High
**發現時間**: 2025-10-31 (瀏覽器控制台)
**問題類型**: UI 元件衝突

#### 問題描述
瀏覽器控制台出現大量相同錯誤：
```
An invalid form control with name='Content' is not focusable.
```

#### 根本原因
- HTML5 的 `required` 屬性嘗試驗證 Content textarea
- EasyMDE 隱藏了原始 textarea，建立自己的編輯器
- 瀏覽器無法對隱藏的 required 欄位進行驗證

#### 原始代碼
```html
<textarea id="Content"
          asp-for="Content"
          rows="15"
          class="..."
          required></textarea> <!-- ❌ 與 EasyMDE 衝突 -->
```

#### 解決方案
移除 `required` 屬性，依賴伺服器端驗證：
```html
<textarea id="Content"
          asp-for="Content"
          rows="15"
          class="..."></textarea>
```

#### 修復檔案
- `Pages/Admin/Posts/Create.cshtml:47-51`
- `Pages/Admin/Posts/Edit.cshtml:30-31`

#### 說明
- 伺服器端驗證（透過 `[Required]` 屬性）仍然有效
- 移除 HTML5 驗證避免與 EasyMDE 衝突

---

### 問題 #6: 容器 DNS 解析失敗

**嚴重程度**: 🔴 Critical
**發現時間**: 2025-10-31 (OAuth 登入時)
**問題類型**: 基礎設施配置

#### 問題描述
OAuth 驗證時出現網絡連線錯誤：
```
System.Net.Sockets.SocketException: Resource temporarily unavailable
Unable to connect to oauth2.googleapis.com:443
```

#### 根本原因
- Podman 容器使用內部 DNS (10.89.12.1)
- 內部 DNS 無法解析外部域名
- Bridge network 模式的網絡隔離問題

#### 解決方案
將 app 容器從 bridge network 改為 host network：

**docker-compose.yml 修改**:
```yaml
app:
  # network_mode: bridge  # ❌ 移除
  network_mode: host      # ✅ 使用主機網絡
  # ...其他配置不變
```

#### 修復檔案
- `docker-compose.yml:33`

#### 權衡考量
- ✅ 優點：容器可以直接訪問外部網絡
- ⚠️ 缺點：失去網絡隔離，容器直接暴露在主機網絡
- 📝 建議：生產環境應使用正確配置的 bridge network + DNS 設定

---

### 問題 #7: Tag 模型缺少 UpdatedAt 屬性導致建立文章失敗

**嚴重程度**: 🔴 Critical
**發現時間**: 2025-10-31 (建立新文章時)
**問題類型**: 資料模型不一致

#### 問題描述
建立帶有標籤的文章時出現 HTTP 500 錯誤：
```
System.InvalidOperationException: The property 'Tag.UpdatedAt' could not be found.
Ensure that the property exists and has been included in the model.
```

#### 根本原因
- `ApplicationDbContext.UpdateTimestamps()` 假設所有實體都有 `UpdatedAt` 屬性
- `Tag` 模型只有 `CreatedAt`，沒有 `UpdatedAt`
- 當 SaveChanges 嘗試更新 Tag 的 UpdatedAt 時拋出異常

#### 原始代碼
```csharp
private void UpdateTimestamps()
{
    foreach (var entry in entries)
    {
        // ...
        if (entry.Property("UpdatedAt").CurrentValue is DateTime)  // ❌ 未檢查屬性存在
        {
            entry.Property("UpdatedAt").CurrentValue = now;
        }
    }
}
```

#### 解決方案
在訪問屬性前先檢查是否存在：
```csharp
private void UpdateTimestamps()
{
    foreach (var entry in entries)
    {
        // Check if entity has CreatedAt property
        var createdAtProperty = entry.Metadata.FindProperty("CreatedAt");
        if (entry.State == EntityState.Added && createdAtProperty != null)
        {
            entry.Property("CreatedAt").CurrentValue = now;
        }

        // Check if entity has UpdatedAt property
        var updatedAtProperty = entry.Metadata.FindProperty("UpdatedAt");
        if (updatedAtProperty != null)
        {
            entry.Property("UpdatedAt").CurrentValue = now;
        }
    }
}
```

#### 修復檔案
- `Data/ApplicationDbContext.cs:106-129`

#### 預防措施
- [ ] 統一所有實體的時間戳欄位（都有或都沒有 UpdatedAt）
- [ ] 通用方法應該始終檢查邊界條件
- [ ] 建立單元測試覆蓋不同實體類型的 SaveChanges 操作

---

## 問題分類分析

### 按類型分類

| 類型 | 數量 | 佔比 |
|---|---|---|
| 缺失檔案 | 2 | 28.6% |
| 依賴/配置錯誤 | 2 | 28.6% |
| 邏輯錯誤 | 2 | 28.6% |
| 基礎設施 | 1 | 14.3% |

### 按嚴重程度分類

| 嚴重程度 | 數量 | 佔比 |
|---|---|---|
| 🔴 Critical | 5 | 71.4% |
| 🟡 High | 2 | 28.6% |

### 按發現階段分類

| 階段 | 數量 | 說明 |
|---|---|---|
| 功能測試 | 3 | OAuth 登入流程測試 |
| UI 測試 | 2 | 瀏覽器控制台錯誤 |
| 資料操作 | 1 | 建立文章測試 |
| 部署測試 | 1 | 容器網絡配置 |

---

## 根本原因分析

### 為什麼這些問題無法在計劃階段預防？

#### 1. **實作細節的不可預見性**
- **缺失視圖檔案**: 計劃階段關注功能流程，不會詳細到每個檔案
- **依賴順序**: 需要實際載入頁面才能發現 JavaScript 依賴問題

#### 2. **第三方元件行為**
- **EasyMDE 衝突**: 需要整合第三方元件後才能發現與 HTML5 驗證的衝突
- **OAuth 限制**: Google OAuth 不支援 SignOut 是提供商特定行為

#### 3. **環境特定問題**
- **容器網絡**: Podman 的 DNS 配置問題是部署環境特定的

#### 4. **設計假設錯誤**
- **UpdateTimestamps**: 假設所有實體都有相同屬性是設計時的錯誤假設

### 方法論的局限性

| BMAD 可以做到 | BMAD 無法做到 |
|---|---|
| ✅ 規劃功能需求 | ❌ 預見實作細節問題 |
| ✅ 設計系統架構 | ❌ 發現第三方元件衝突 |
| ✅ 定義資料模型 | ❌ 防止錯誤的設計假設 |
| ✅ 排定開發優先序 | ❌ 預測環境配置問題 |

---

## 改進建議

### 1. 開發流程改進

#### 引入更早的驗證階段
```
完成一個 Epic → 立即整合測試 → 發現問題 → 修復
（而非等到所有 Story 完成）
```

**具體做法**:
- Epic 1 (基礎系統) 完成後立即部署測試
- Epic 2 (文章管理) 完成後測試 CRUD 操作
- Epic 3 (進階功能) 完成後進行完整功能測試

### 2. Definition of Done 擴充

現有:
- ✅ 功能實現
- ✅ 程式碼審查

建議新增:
- ✅ **視圖檔案完整性檢查**（每個 PageModel 有對應 View）
- ✅ **瀏覽器控制台無錯誤**
- ✅ **容器環境測試通過**
- ✅ **整合測試通過**

### 3. 技術債務檢查清單

建立 `TECH_DEBT_CHECKLIST.md`:

**檔案完整性**
- [ ] 每個 PageModel (.cshtml.cs) 都有對應的 View (.cshtml)
- [ ] 所有 partial views 都存在

**前端依賴**
- [ ] JavaScript 依賴順序正確（jQuery → Plugins → App）
- [ ] CSS 依賴順序正確
- [ ] 第三方元件初始化順序正確

**資料模型一致性**
- [ ] 所有實體的時間戳欄位一致
- [ ] 導航屬性正確配置
- [ ] 索引和約束完整

**通用方法安全性**
- [ ] 參數 null 檢查
- [ ] 屬性存在性檢查
- [ ] 邊界條件處理

**環境配置**
- [ ] 本地開發環境測試通過
- [ ] 容器環境測試通過
- [ ] 網絡連線正常（外部 API）

### 4. 自動化測試擴充

#### 檢測缺失視圖檔案
```csharp
[Theory]
[InlineData(typeof(CreateModel), "Create.cshtml")]
[InlineData(typeof(EditModel), "Edit.cshtml")]
[InlineData(typeof(CallbackModel), "Callback.cshtml")]
[InlineData(typeof(LogoutModel), "Logout.cshtml")]
public void PageModel_Should_Have_Corresponding_View(Type pageModelType, string viewName)
{
    var pageModelName = pageModelType.Name;
    var expectedViewPath = Path.Combine("Pages", "Admin", "Posts", viewName);
    Assert.True(File.Exists(expectedViewPath),
        $"PageModel {pageModelName} is missing view file: {viewName}");
}
```

#### 檢測資料模型一致性
```csharp
[Fact]
public void All_Entities_With_CreatedAt_Should_Have_UpdatedAt()
{
    var entityTypes = typeof(ApplicationDbContext).Assembly
        .GetTypes()
        .Where(t => t.Namespace?.Contains("Data.Entities") == true);

    foreach (var entityType in entityTypes)
    {
        var hasCreatedAt = entityType.GetProperty("CreatedAt") != null;
        var hasUpdatedAt = entityType.GetProperty("UpdatedAt") != null;

        if (hasCreatedAt)
        {
            Assert.True(hasUpdatedAt,
                $"Entity {entityType.Name} has CreatedAt but missing UpdatedAt");
        }
    }
}
```

### 5. 文件化最佳實踐

建立 `DEVELOPMENT_GUIDELINES.md`:

**Razor Pages 開發規範**
1. 建立 PageModel 時同時建立 View 檔案
2. View 檔案應至少包含最基本的 HTML 結構
3. 在 git commit 前檢查檔案配對完整性

**JavaScript 開發規範**
1. 依賴順序：jQuery → jQuery Plugins → Third-party Libraries → App Code
2. 在頁面載入後檢查控制台是否有錯誤
3. 使用 CDN 時準備 fallback 本地版本

**Entity Framework 開發規範**
1. 通用方法訪問屬性前先檢查存在性
2. 時間戳欄位保持一致（都有或都沒有）
3. Migration 前檢查模型一致性

**容器開發規範**
1. 修改源碼後必須重新 build 容器
2. 不能只 restart，要 build + up
3. 測試外部 API 連線（DNS 解析）

---

## 經驗教訓

### ✅ 做得好的部分

1. **快速發現與修復**: 所有問題在測試階段立即發現並修復
2. **系統化記錄**: 詳細記錄每個問題的根本原因和解決方案
3. **持續改進**: 從問題中學習，提出具體改進措施

### 🔄 需要改進的部分

1. **測試時機**: 應該在每個 Epic 完成後立即測試
2. **檢查清單**: 缺少系統化的檢查清單（已補充）
3. **自動化測試**: 應該有更多自動化測試覆蓋

### 💡 關鍵洞察

> **方法論不能保證完美，但能提供框架讓我們快速發現和解決問題。**

這次經驗證明：
- 計劃階段無法預見所有細節問題 ✅ 正常
- 快速迭代可以快速發現問題 ✅ 有效
- 系統化的檢查清單可以減少問題 ✅ 必要

敏捷開發的精髓是：
```
計劃 → 實作 → 測試 → 發現問題 → 修復 → 持續改進
```

---

## 結論

本次開發遇到的 7 個問題都已成功修復，所有問題都屬於**實作細節和環境配置**層面，無法在計劃階段預防。

這不代表 BMAD 方法論失敗，而是展現了**敏捷開發的真實樣貌**：
- ✅ 方法論提供了清晰的開發框架
- ✅ 迭代開發讓問題能快速暴露
- ✅ 及時測試確保問題早期發現
- ✅ 系統化記錄支持持續改進

透過這次經驗，我們建立了更完善的開發流程和檢查機制，為後續開發奠定更堅實的基礎。

---

**報告結束**

最後更新: 2025-10-31
狀態: 所有問題已修復 ✅
