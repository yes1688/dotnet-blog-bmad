# dotnet-blog-bmad

個人部落格系統 - 使用 ASP.NET Core 8.0 Razor Pages 建構

## 專案簡介

這是一個功能完整的個人部落格系統，採用 .NET 8.0 Razor Pages 框架開發，使用 PostgreSQL 作為資料庫。專案遵循 BMAD 方法論進行開發。

### 核心功能

- 使用 Google OAuth 2.0 進行管理者身份驗證
- Markdown 格式的文章撰寫與編輯 (使用 EasyMDE)
- 響應式設計，支援桌面與行動裝置
- 文章標籤與分類管理
- SEO 友善的 URL 結構
- 程式碼語法高亮 (使用 Prism.js)

## 技術架構

### 後端技術

- **框架**: ASP.NET Core 8.0 (Razor Pages)
- **資料庫**: PostgreSQL 15
- **ORM**: Entity Framework Core 9.0
- **Markdown 處理**: Markdig
- **身份驗證**: Microsoft.AspNetCore.Authentication.Google

### 前端技術

- **CSS 框架**: Tailwind CSS
- **Markdown 編輯器**: EasyMDE
- **語法高亮**: Prism.js
- **圖示**: Lucide Icons

## 專案結構

```
dotnet-blog-bmad/
├── Pages/                      # Razor Pages (UI 層)
│   ├── Index.cshtml           # 首頁
│   ├── Admin/                 # 管理後台頁面
│   └── Shared/                # 共用佈局與元件
├── Services/                   # 業務邏輯層
│   └── Interfaces/            # 服務介面定義
├── Data/                      # 資料存取層
│   ├── Entities/              # Entity 模型
│   ├── Configurations/        # EF Core Fluent API 配置
│   └── ApplicationDbContext.cs
├── Models/                    # DTO 與 ViewModel
│   ├── DTOs/                  # 資料傳輸物件
│   └── ViewModels/            # 頁面視圖模型
├── Extensions/                # 擴充方法
├── wwwroot/                   # 靜態資源 (CSS, JS, images)
├── docs/                      # 專案文件
│   ├── brief.md              # 專案簡介
│   ├── prd.md                # 產品需求文件
│   └── architecture.md       # 架構設計文件
└── .bmad-core/               # BMAD 框架檔案
```

## 快速開始

### 前置需求

- Podman 與 Podman Compose (或 Docker 與 Docker Compose)
- Git

### 本地開發環境設定

#### 方法一: 使用 Podman Compose (推薦)

完整的容器化開發環境，包含應用程式與 PostgreSQL 資料庫。

1. **Clone 專案**

```bash
git clone <repository-url>
cd dotnet-blog-bmad
```

2. **設定環境變數**

```bash
# 複製環境變數範本
cp .env.example .env

# 編輯 .env 檔案，設定必要的環境變數
# 特別是 Google OAuth 憑證與管理員白名單
nano .env
```

3. **啟動完整環境**

```bash
# 建置並啟動應用程式與資料庫
podman compose up -d

# 查看容器狀態
podman compose ps

# 查看應用程式日誌
podman compose logs -f app
```

4. **瀏覽應用程式**

開啟瀏覽器訪問: `http://localhost:5000`

5. **停止環境**

```bash
# 停止並移除容器
podman compose down

# 停止並移除容器與 volumes (清除資料庫)
podman compose down -v
```

#### 方法二: 使用 Podman 直接執行 (僅測試)

適合快速測試，不包含資料庫。

```bash
# 使用 .NET SDK 容器執行專案
podman run --rm -it \
  -v "$(pwd):/app" \
  -w /app \
  -p 5000:5000 \
  -e ASPNETCORE_URLS="http://+:5000" \
  mcr.microsoft.com/dotnet/sdk:8.0 \
  dotnet run
```

## 開發狀態

目前專案處於初始開發階段，正在實作以下功能:

- [x] Story 1.1: 專案初始化與資料夾結構
- [x] Story 1.2: Container 開發環境設定
- [ ] Story 1.3: Entity Framework Core 設定
- [ ] Story 1.4: Google OAuth 身份驗證
- [ ] Story 1.5: 基礎頁面與導覽

完整的功能規劃請參閱 [PRD 文件](docs/prd.md)。

## 文件

- [專案簡介 (Project Brief)](docs/brief.md)
- [產品需求文件 (PRD)](docs/prd.md)
- [架構設計文件 (Architecture)](docs/architecture.md)

## 開發方法論

本專案採用 **BMAD (Business Model-Aligned Development)** 方法論:

1. **Planning Phase**: 需求分析 → PRD → 架構設計
2. **Development Phase**: Story-by-Story 實作與測試
3. **Quality Assurance**: 持續驗證與迭代

## 授權

本專案僅供個人學習與使用。

## 聯絡資訊

如有任何問題或建議，請透過 Issues 回報。
