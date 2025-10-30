# .NET 個人部落格系統 Product Requirements Document (PRD)

## Goals and Background Context

### Goals

- 建立一個設計優先、效能卓越的個人部落格平台，提供優雅的閱讀體驗
- 實現簡化的單一管理員認證機制（Google OAuth + 環境變數白名單）
- 提供直觀的 Markdown 內容管理系統，讓創作者專注於內容創作
- 達成卓越的 SEO 表現與頁面載入速度（Lighthouse Performance > 90, SEO > 95）
- 在 3-6 個月內完成 MVP 開發並正式上線

### Background Context

現有部落格平台普遍存在設計過時、載入緩慢、權限管理過於複雜的問題，不適合個人部落客使用。WordPress 自架過於複雜需要維護外掛，靜態網站產生器缺乏友善的管理介面，Medium/Substack 無法完全自訂設計且受限於平台規則。

本專案旨在打造一個輕量級的 .NET 部落格系統，專注於設計優先、簡化認證（Google OAuth + 環境變數白名單）、核心 CRUD 功能，避免功能膨脹。利用 .NET 的高效能與成熟生態系，配合清晰的設計系統（白/藍配色）確保視覺一致性，讓創作者能夠專注於內容本身，成為技術部落客與內容創作者的首選自架部落格解決方案。

### Change Log

| Date | Version | Description | Author |
|------|---------|-------------|--------|
| 2025-10-30 | v1.0 | Initial PRD creation from Project Brief | PM (John) |

---

## Requirements

### Functional Requirements

**FR1: Google OAuth 認證與白名單機制**
- 系統必須整合 Google OAuth 2.0 進行使用者認證
- 透過環境變數（`ALLOWED_ADMIN_EMAIL`）維護管理員白名單
- 只允許白名單內的 Google 帳號存取後台管理功能
- 非白名單使用者僅能瀏覽前台公開內容
- 提供登入/登出功能與 Session 管理

**FR2: 文章 CRUD 管理**
- 管理員可以建立、讀取、更新、刪除文章
- 文章包含以下欄位：標題、內容（Markdown 格式）、Slug、發布狀態、發布時間、建立時間、更新時間
- 所有 CRUD 操作僅限白名單管理員存取

**FR3: Markdown 編輯器整合**
- 後台新增/編輯文章頁面必須整合 Markdown 編輯器（EasyMDE）
- 編輯器需提供工具列、鍵盤快捷鍵、即時預覽功能
- 編輯器樣式需符合專案設計系統

**FR4: 中文 Slug 支援**
- 系統必須支援中文 URL Slug（使用 UTF-8 編碼）
- 建立文章時自動從標題生成 Slug（支援中文、英文、數字、連字號）
- 管理員可以手動修改自動生成的 Slug
- Slug 必須在資料庫中唯一

**FR5: 發布狀態管理**
- 文章必須支援兩種狀態：草稿（Draft）、已發布（Published）
- 草稿狀態的文章不會在前台顯示
- 已發布文章需記錄發布時間（PublishedAt）
- 管理員可以在後台切換文章的發布狀態

**FR6: Markdown 處理與渲染**
- 使用 Markdig 處理 Markdown 內容並轉換為 HTML
- 支援 CommonMark 標準與 GitHub Flavored Markdown (GFM)
- 支援程式碼區塊、表格、任務清單等擴充功能
- 實作 XSS 防護，清理不安全的 HTML 標籤

**FR7: 程式碼語法高亮**
- 程式碼區塊必須支援語法高亮顯示
- 使用 Prism.js 進行前端語法高亮處理
- 程式碼字型使用 JetBrains Mono
- 支援常見程式語言（JavaScript, Python, C#, Java, Go, Rust 等）

**FR8: 前台文章列表頁**
- 顯示所有已發布文章的清單（URL: `/posts`）
- 每篇文章顯示：標題、摘要（從內容截取前 150 字）、發布時間
- 實作傳統分頁導航（每頁 10 篇文章）
- 分頁 URL 格式：`/posts` (第1頁) 或 `/posts?page=2`
- 提供分頁導航元件：← 上一頁 [1] [2] [3] ... 下一頁 →

**FR9: 前台文章內容頁**
- 提供獨立的文章閱讀頁面（URL: `/posts/{slug}`）
- 顯示完整的文章內容（Markdown 渲染後的 HTML）
- 顯示文章標題、發布時間（格式：YYYY-MM-DD HH:mm:ss）
- 文章內容區最大寬度限制為 720px（適合閱讀）
- 支援中文 Slug URL

**FR10: 圖片外部連結支援（MVP）**
- 文章內容支援使用 Markdown 標準語法嵌入圖片：`![alt](url)`
- 圖片 URL 可以是外部圖床連結（Imgur, Cloudinary）或專案內靜態圖片路徑
- MVP 階段不提供圖片上傳功能

**FR11: SEO 基礎功能**
- 每個頁面必須包含獨特的 `<title>` 和 `<meta name="description">`
- 實作 Open Graph Tags（Facebook、LinkedIn 分享預覽）
- 實作 Twitter Card Tags（Twitter 分享預覽）
- 實作 Canonical URL（避免重複內容問題）
- 使用語意化 HTML 標籤（`<article>`, `<header>`, `<main>`, `<nav>`）

**FR12: 響應式設計實作**
- 網站必須完整支援桌面、平板、手機裝置
- 使用 Tailwind CSS 的響應式斷點系統
- 在所有裝置上提供良好的閱讀體驗與操作流暢度
- 確保觸控操作友善（按鈕尺寸足夠、間距充足）

**FR13: 設計系統實作**
- 實作完整的設計系統（Azure Blue #0078D4 + 白色系統）
- 套用 Design Tokens：顏色、字型、間距、圓角、陰影、動畫
- 使用 Noto Sans TC（內文）與 JetBrains Mono（程式碼）字型
- 所有 UI 元件遵循統一的設計規範

**FR14: 時區處理**
- 資料庫以 UTC 時間儲存所有時間戳記
- 前台顯示時轉換為台灣時間（UTC+8）
- 時間顯示格式：`YYYY-MM-DD HH:mm:ss`

### Non-Functional Requirements

**NFR1: 效能要求**
- 首屏載入時間（First Contentful Paint）< 1.5 秒
- 頁面完全載入時間 < 2 秒
- Time to Interactive < 3.5 秒
- Lighthouse Performance Score > 90

**NFR2: SEO 效能**
- Lighthouse SEO Score > 95
- 所有頁面必須可被搜尋引擎爬取與索引
- 確保結構化的 HTML 與正確的 Meta Tags

**NFR3: 可用性與無障礙**
- Lighthouse Accessibility Score > 90
- 文字與背景對比度符合 WCAG AA 標準（4.5:1）
- 支援鍵盤導航與 Screen Reader

**NFR4: 安全性要求**
- 必須強制使用 HTTPS
- 實作 CSRF 防護（ASP.NET Core 內建）
- 實作 XSS 防護（Markdown 內容清理）
- 敏感資訊（OAuth Secrets, Database Passwords）必須透過環境變數管理，不得寫入程式碼或 Docker 映像檔
- 通過容器安全掃描（Trivy 或類似工具）

**NFR5: 容器化部署**
- 應用程式必須可透過 Docker/Podman 容器部署
- 提供 Dockerfile（多階段建置，Debian 基底）
- 提供 docker-compose.yml（應用 + PostgreSQL）
- 確保 Podman 完全相容（不使用 Docker 專屬功能）
- 資料庫資料必須透過 Volume 持久化

**NFR6: 可維護性**
- 程式碼結構清晰，遵循分層架構（Presentation, Business, Data）
- 關鍵業務邏輯需撰寫單元測試
- 資料庫 Schema 變更需透過 EF Core Migrations 管理
- 提供清楚的 README 文件與環境設定說明

**NFR7: 可擴展性**
- 架構設計需預留未來擴展空間（分類、標籤、搜尋、圖片上傳）
- 資料模型設計需支援未來功能需求
- 容器化架構支援平台遷移（Fly.io, Railway, Azure, AWS 等）

---

## User Interface Design Goals

### Overall UX Vision

本專案採用**設計優先**理念，打造清新、專業、優雅的閱讀體驗。UI/UX 不是附加功能，而是產品的核心競爭力。設計風格以「現代極簡」為主軸，強調充足的留白、清晰的視覺層級、微妙的互動回饋，讓讀者能夠專注於內容本身，不受干擾。

後台管理介面同樣遵循設計系統，提供直觀的內容管理體驗，讓管理員無需查閱文件即可完成基本操作。

### Key Interaction Paradigms

- **卡片式設計（Card-based Design）：** 文章列表使用卡片元件呈現，配合微陰影與 Hover 動畫提升互動性
- **流暢動畫（Smooth Transitions）：** 所有互動（按鈕、卡片 Hover、頁面切換）使用 250ms 的標準過渡動畫
- **Focus 狀態清晰：** 表單輸入框與按鈕的 Focus 狀態使用 Azure Blue 外框高亮，支援鍵盤導航
- **響應式導航：** 桌面使用水平導航列，手機使用 Hamburger Menu（漢堡選單）

### Core Screens and Views

**前台（Public Front-End）：**
1. **首頁（Homepage）** - 歡迎訊息 + 最新文章列表預覽
2. **文章列表頁（`/posts`）** - 分頁式文章清單，每篇顯示標題、摘要、發布時間
3. **文章內容頁（`/posts/{slug}`）** - 完整文章內容，最大寬度 720px，優雅的排版

**後台（Admin Backend）：**
4. **後台首頁（`/admin`）** - Dashboard 顯示文章統計與快速操作
5. **文章管理列表（`/admin/posts`）** - 顯示所有文章（草稿 + 已發布），提供編輯/刪除操作
6. **新增文章（`/admin/posts/create`）** - Markdown 編輯器，標題、內容、Slug 輸入
7. **編輯文章（`/admin/posts/edit/{id}`）** - 與新增頁面類似，預填現有內容

**通用頁面：**
8. **登入頁（`/login`）** - Google OAuth 登入按鈕
9. **錯誤頁（404, 403, 500）** - 友善的錯誤訊息與返回首頁連結

### Accessibility

**WCAG AA 標準**
- 確保所有文字與背景對比度 ≥ 4.5:1
- 支援鍵盤 Tab 導航
- 為互動元件提供適當的 ARIA 標籤
- 圖片必須包含 `alt` 屬性
- 表單輸入框需關聯 `<label>`

### Branding

- **核心配色：** Azure Blue (#0078D4) + 白色系統
- **風格定位：** 現代專業、清新簡約、技術感
- **設計靈感：** Microsoft Fluent Design、Ghost CMS、Medium
- **視覺特徵：** 微陰影（subtle shadows）、流暢動畫、充足留白、清晰層級

### Target Device and Platforms

**Web Responsive（跨裝置響應式設計）**
- **桌面（Desktop）：** 1280px+ 寬螢幕，最佳閱讀體驗
- **平板（Tablet）：** 768px - 1024px，適應中等螢幕尺寸
- **手機（Mobile）：** 320px - 767px，優化觸控操作與單欄佈局

**支援的瀏覽器：**
- Chrome, Firefox, Safari, Edge（最新兩個版本）
- iOS Safari, Chrome Mobile（最新版本）

---

## Technical Assumptions

### Repository Structure: Monorepo

本專案採用**單一 Monorepo** 架構，ASP.NET Core Razor Pages 應用整合前後端功能。這種架構簡化開發與部署流程，適合 MVP 階段快速迭代。

**資料夾結構：**
```
dotnet-blog-bmad/
├── Pages/               # Razor Pages (前台 + 後台)
├── Services/            # Business Logic
├── Data/                # EF Core Models & DbContext
├── wwwroot/             # 靜態資源 (CSS, JS, 圖片)
├── Dockerfile           # 容器建置設定
├── docker-compose.yml   # 本地開發服務編排
└── appsettings.json     # 應用程式設定
```

### Service Architecture

**Monolith（單體應用）**

選擇單體架構的理由：
- **開發簡單：** 前後端整合在同一專案，減少跨服務通訊複雜度
- **部署簡單：** 單一容器即可運行整個應用
- **效能優越：** 無需 API 呼叫開銷，Razor Pages 伺服器端渲染效率高
- **SEO 友善：** 完整 HTML 由伺服器生成，搜尋引擎完美支援
- **適合 MVP：** 個人部落格流量有限，不需要微服務的複雜性

**技術堆疊：**
- **Frontend:** ASP.NET Core Razor Pages + Tailwind CSS + Alpine.js（輕量互動）
- **Backend:** ASP.NET Core 8.0 + Entity Framework Core
- **Database:** PostgreSQL 15
- **Authentication:** Google OAuth 2.0

### Testing Requirements

**Unit + Integration Testing**

- **Unit Tests：** 針對 Services 層的業務邏輯（Markdown 處理、Slug 生成、時區轉換）
- **Integration Tests：** 針對 Razor Pages 的端到端測試（文章 CRUD、認證流程）
- **測試框架：** xUnit + FluentAssertions
- **測試覆蓋目標：** 關鍵業務邏輯 > 80% 覆蓋率

**測試範圍：**
- Markdown 轉 HTML 正確性
- 中文 Slug 生成邏輯
- Google OAuth 白名單驗證
- 文章分頁邏輯
- 時區轉換正確性
- XSS 防護有效性

### Additional Technical Assumptions and Requests

**容器化開發環境：**
- 使用 **Docker/Podman** 進行本地開發與部署
- 確保 Podman 完全相容（不使用 Docker 專屬功能）
- 提供 `docker-compose.yml` 進行應用 + PostgreSQL 的服務編排
- 支援 Hot Reload 在容器內運作（開發階段）

**容器基底映像（已確定）：**
- **Debian 基底：** 使用 `mcr.microsoft.com/dotnet/aspnet:8.0`（Debian）
- **選擇理由：** 穩定、相容性好、問題少、除錯容易
- **映像檔大小：** 約 230-250MB（可接受，未來可優化為 Alpine）

**Markdown 編輯器（已確定）：**
- **EasyMDE：** JavaScript Markdown 編輯器（SimpleMDE 的活躍 fork）
- **特色：** 工具列、快捷鍵、即時預覽、自訂樣式
- **整合方式：** 透過 CDN 或 npm 安裝，在 Razor Pages 中引入

**Markdown 處理器（已確定）：**
- **Markdig：** C# Markdown 處理器
- **支援：** CommonMark + GitHub Flavored Markdown (GFM)
- **擴充功能：** 程式碼高亮、表格、任務清單

**程式碼高亮（已確定）：**
- **Prism.js：** 輕量級前端語法高亮庫
- **主題：** 選擇與設計系統協調的配色主題
- **字型：** JetBrains Mono

**分頁策略（已確定）：**
- **傳統分頁：** 每頁 10 篇文章
- **URL 格式：** `/posts` (第1頁) 或 `/posts?page=2`
- **不使用無限滾動**

**時區處理（已確定）：**
- **儲存：** 資料庫以 UTC 時間儲存
- **顯示：** 轉換為台灣時間（UTC+8）
- **格式：** `YYYY-MM-DD HH:mm:ss`

**Web 伺服器：**
- **ASP.NET Core Kestrel：** 內建 Web 伺服器，直接對外服務
- **不需要 NGINX：** MVP 階段 Kestrel 已足夠，HTTPS 由 Kestrel 處理
- **未來可選：** 若需要反向代理（SSL Termination, Load Balancing），可加入 NGINX

**環境變數管理：**
- 敏感資訊（OAuth Secrets, Database Connection String, Admin Email Whitelist）透過環境變數注入
- 提供 `.env.example` 範本
- 使用 `docker-compose` 的 `env_file` 或 `.env` 檔案

**資料庫 Schema：**
```csharp
// Post Entity
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; } // Unique, supports Chinese
    public string Content { get; set; } // Markdown
    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; } // UTC
    public DateTime CreatedAt { get; set; } // UTC
    public DateTime UpdatedAt { get; set; } // UTC
}

// Admin Entity
public class Admin
{
    public int Id { get; set; }
    public string Email { get; set; } // Google account email
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } // UTC
}
```

**SEO 實作細節：**
- 每個 Razor Page 可自訂 `<title>` 和 `<meta name="description">`
- 使用 `ViewData` 或 Page Model Properties 傳遞 SEO 資訊
- 建立 SEO Helper 或 Base Page Model 統一管理 Meta Tags

**字型載入策略：**
- **Noto Sans TC：** 從 Google Fonts CDN 載入，使用 `font-display: swap` 避免 FOIT
- **JetBrains Mono：** 從 Google Fonts 或本地載入
- 使用 `<link rel="preconnect">` 優化字型載入效能

---

## Epic List

以下是專案的高階 Epic 規劃，每個 Epic 都代表一個完整的、可部署的功能增量。

**Epic 1: Foundation & Core Infrastructure**
建立專案基礎設施，包含專案初始化、容器化開發環境、資料庫設定、基礎 Razor Pages 架構，以及 Google OAuth 認證機制。此 Epic 完成後，應用可以成功運行並提供基本的認證功能與健康檢查端點。

**Epic 2: Content Management System (Backend)**
實作後台內容管理系統，包含文章 CRUD 功能、Markdown 編輯器整合（EasyMDE）、中文 Slug 生成邏輯、發布狀態管理。此 Epic 完成後，管理員可以透過後台完整管理文章內容。

**Epic 3: Public Front-End Display**
實作前台公開展示功能，包含文章列表頁（含分頁）、文章內容頁、Markdown 渲染、程式碼高亮、SEO Meta Tags。此 Epic 完成後，讀者可以瀏覽已發布的文章並享受良好的閱讀體驗。

**Epic 4: Design System & Responsive Polish**
實作完整的設計系統，包含 Tailwind CSS 整合、Design Tokens 配置、響應式佈局、UI 元件樣式、字型載入優化、效能調校。此 Epic 完成後，應用達到設計規範要求與效能目標（Lighthouse > 90）。

---

## Epic 1: Foundation & Core Infrastructure

**Epic Goal:**
建立專案的核心基礎設施，包含專案初始化、容器化開發環境（Podman/Docker + PostgreSQL）、資料庫 ORM 設定、基礎 Razor Pages 架構，以及 Google OAuth 2.0 認證機制。完成後，應用程式可以在容器中運行，管理員可以透過 Google OAuth 登入後台，並提供健康檢查端點驗證系統運作正常。

### Story 1.1: 專案初始化與資料夾結構

**As a** 開發者，
**I want** 建立 .NET 8 Razor Pages 專案並設定清晰的資料夾結構，
**so that** 專案有良好的組織架構，便於後續開發與維護。

**Acceptance Criteria:**
1. 使用 `dotnet new razor -n dotnet-blog-bmad` 建立 ASP.NET Core Razor Pages 專案
2. 安裝必要的 NuGet 套件：
   - `Npgsql.EntityFrameworkCore.PostgreSQL`
   - `Markdig`
   - `Microsoft.AspNetCore.Authentication.Google`
3. 建立資料夾結構：`Pages/`, `Services/`, `Data/`, `wwwroot/`
4. 初始化 Git repository 並建立 `.gitignore`
5. 建立 `README.md` 說明專案目的與設定步驟
6. 專案可以執行 `dotnet run` 並顯示預設的 Razor Pages 頁面

### Story 1.2: 容器化開發環境設定

**As a** 開發者，
**I want** 建立 Dockerfile 與 docker-compose.yml 設定容器化開發環境，
**so that** 應用程式與 PostgreSQL 資料庫可以在容器中運行，確保開發/生產環境一致。

**Acceptance Criteria:**
1. 建立 `Dockerfile`（多階段建置）：
   - Stage 1: 使用 `mcr.microsoft.com/dotnet/sdk:8.0` 建置應用
   - Stage 2: 使用 `mcr.microsoft.com/dotnet/aspnet:8.0`（Debian 基底）執行應用
2. 建立 `docker-compose.yml` 包含兩個服務：
   - `app`: ASP.NET Core 應用（port 5000/5001）
   - `db`: PostgreSQL 15（port 5432）
3. 設定 `postgres-data` Volume 進行資料持久化
4. 建立 `.env.example` 範本包含：資料庫連線字串、OAuth Secrets、白名單 Email
5. 執行 `podman-compose up` 或 `docker-compose up` 可成功啟動應用與資料庫
6. 驗證容器內應用可以連線到 PostgreSQL 資料庫

### Story 1.3: Entity Framework Core 與資料模型設定

**As a** 開發者，
**I want** 設定 Entity Framework Core 並定義 Post 與 Admin 資料模型，
**so that** 應用程式可以透過 ORM 操作資料庫。

**Acceptance Criteria:**
1. 在 `Data/` 資料夾建立 `ApplicationDbContext.cs`
2. 定義 `Post` Entity：
   - 欄位：Id, Title, Slug (unique), Content, IsPublished, PublishedAt, CreatedAt, UpdatedAt
   - 所有時間欄位使用 UTC
3. 定義 `Admin` Entity：
   - 欄位：Id, Email (unique), Name, CreatedAt
4. 在 `Program.cs` 註冊 DbContext，連線字串從環境變數讀取（`ConnectionStrings__DefaultConnection`）
5. 安裝 EF Tools：`dotnet tool install --global dotnet-ef`
6. 執行 Migration：`dotnet ef migrations add InitialCreate`
7. 執行 `dotnet ef database update` 成功建立資料庫 Schema
8. 驗證 PostgreSQL 資料庫中存在 `Posts` 與 `Admins` 資料表

### Story 1.4: Google OAuth 2.0 認證整合

**As a** 管理員，
**I want** 透過 Google OAuth 登入後台，
**so that** 只有授權的 Google 帳號可以存取內容管理系統。

**Acceptance Criteria:**
1. 在 Google Cloud Console 建立 OAuth 2.0 憑證（Client ID, Client Secret）
2. 在 `Program.cs` 設定 Google Authentication Middleware
3. 實作環境變數 `ALLOWED_ADMIN_EMAIL` 白名單檢查邏輯
4. 建立 `/login` 頁面顯示「使用 Google 登入」按鈕
5. 實作 `/logout` 功能清除 Session
6. 建立 Authorization Policy 保護 `/admin/*` 路由
7. 白名單內的 Google 帳號可以成功登入並存取 `/admin` 頁面
8. 非白名單帳號嘗試登入時顯示「未授權」錯誤訊息
9. 未登入使用者存取 `/admin` 路由時自動重導向到 `/login`

### Story 1.5: 基礎 Razor Pages 架構與健康檢查

**As a** 開發者，
**I want** 建立基礎的 Razor Pages 架構與健康檢查端點，
**so that** 專案有清楚的頁面結構與可驗證系統運作狀態的端點。

**Acceptance Criteria:**
1. 建立 `Pages/Shared/_Layout.cshtml` 基礎佈局（暫時使用預設樣式）
2. 建立 `Pages/Index.cshtml` 首頁顯示「歡迎使用 .NET 個人部落格」
3. 建立 `Pages/Admin/Index.cshtml` 後台首頁顯示「後台管理」（需登入）
4. 建立健康檢查端點 `/health` 返回 JSON：`{"status": "healthy", "database": "connected"}`
5. 健康檢查端點需驗證資料庫連線狀態
6. 執行應用後可以透過瀏覽器存取 `/` 與 `/health` 端點
7. 存取 `/admin` 需要先登入，否則重導向到 `/login`

---

## Epic 2: Content Management System (Backend)

**Epic Goal:**
實作完整的後台內容管理系統，讓管理員可以建立、編輯、刪除文章，整合 Markdown 編輯器（EasyMDE），實作中文 Slug 自動生成邏輯，以及草稿/發布狀態管理。完成後，管理員可以透過直觀的介面完整管理部落格內容。

### Story 2.1: 文章管理列表頁

**As a** 管理員，
**I want** 在後台看到所有文章的列表（包含草稿與已發布），
**so that** 我可以快速瀏覽、編輯或刪除現有文章。

**Acceptance Criteria:**
1. 建立 `Pages/Admin/Posts/Index.cshtml` 文章管理列表頁（URL: `/admin/posts`）
2. 顯示所有文章（草稿 + 已發布）的表格，包含以下欄位：
   - 標題（Title）
   - Slug
   - 狀態（Draft / Published）
   - 發布時間（PublishedAt，草稿顯示為「-」）
   - 更新時間（UpdatedAt）
   - 操作按鈕（編輯、刪除）
3. 已發布文章以綠色標籤顯示「Published」，草稿以灰色標籤顯示「Draft」
4. 提供「新增文章」按鈕連結到 `/admin/posts/create`
5. 點擊「編輯」按鈕跳轉到 `/admin/posts/edit/{id}`
6. 點擊「刪除」按鈕顯示確認對話框，確認後刪除文章並重新整理列表
7. 文章依更新時間降序排序（最新的在最上方）
8. 空文章列表顯示「尚無文章，立即新增第一篇文章」

### Story 2.2: 新增文章頁面（不含編輯器）

**As a** 管理員，
**I want** 建立新文章並輸入標題、內容、Slug，
**so that** 我可以新增部落格內容到系統中。

**Acceptance Criteria:**
1. 建立 `Pages/Admin/Posts/Create.cshtml` 新增文章頁面（URL: `/admin/posts/create`）
2. 表單包含以下欄位：
   - 標題（Title）- 必填，文字輸入框
   - Slug - 選填，文字輸入框（自動生成，可手動修改）
   - 內容（Content）- 必填，多行文字輸入框（暫時使用 `<textarea>`，下一個 Story 整合編輯器）
   - 發布狀態 - 單選（「儲存為草稿」或「立即發布」）
3. 輸入標題時，自動生成 Slug 並填入 Slug 欄位（支援中文、英文、數字、連字號）
4. 管理員可以手動修改自動生成的 Slug
5. 點擊「儲存」按鈕後：
   - 驗證必填欄位（Title, Content）
   - 檢查 Slug 是否唯一（若重複顯示錯誤訊息）
   - 建立新文章記錄，設定 CreatedAt 與 UpdatedAt 為當前 UTC 時間
   - 若選擇「立即發布」，設定 IsPublished=true 與 PublishedAt
   - 重導向到 `/admin/posts` 並顯示成功訊息
6. 點擊「取消」按鈕返回 `/admin/posts`

### Story 2.3: Markdown 編輯器整合（EasyMDE）

**As a** 管理員，
**I want** 使用 Markdown 編輯器撰寫文章內容，
**so that** 我可以方便地格式化文字、插入連結與程式碼區塊。

**Acceptance Criteria:**
1. 在 `Pages/Admin/Posts/Create.cshtml` 與 `Edit.cshtml` 中整合 EasyMDE
2. 將原本的 `<textarea>` 替換為 EasyMDE 編輯器
3. 編輯器需提供工具列包含：粗體、斜體、標題、連結、圖片、程式碼區塊、清單、引言
4. 編輯器支援鍵盤快捷鍵（Ctrl+B 粗體, Ctrl+I 斜體 等）
5. 編輯器提供「預覽」功能（即時 Markdown 渲染）
6. 編輯器樣式需符合專案設計系統（稍後在 Epic 4 調整）
7. 儲存時，編輯器內容正確存入 `Post.Content` 欄位
8. 編輯現有文章時，編輯器正確顯示現有的 Markdown 內容

### Story 2.4: 編輯文章頁面

**As a** 管理員，
**I want** 編輯現有文章的標題、內容、Slug 與發布狀態，
**so that** 我可以更新或修正已發布的文章。

**Acceptance Criteria:**
1. 建立 `Pages/Admin/Posts/Edit.cshtml` 編輯文章頁面（URL: `/admin/posts/edit/{id}`）
2. 頁面載入時，從資料庫讀取文章資料並預填表單欄位
3. 表單結構與新增頁面相同（標題、Slug、內容、發布狀態）
4. 編輯器預載現有的 Markdown 內容
5. 管理員可以修改任何欄位
6. 點擊「儲存」按鈕後：
   - 驗證必填欄位與 Slug 唯一性
   - 更新文章記錄，設定 UpdatedAt 為當前 UTC 時間
   - 若狀態從「草稿」改為「已發布」，設定 PublishedAt 為當前時間
   - 若狀態從「已發布」改為「草稿」，清空 PublishedAt
   - 重導向到 `/admin/posts` 並顯示成功訊息
7. 點擊「取消」按鈕返回 `/admin/posts`
8. 若文章 ID 不存在，顯示 404 錯誤頁面

### Story 2.5: 中文 Slug 自動生成邏輯

**As a** 管理員，
**I want** 系統自動從文章標題生成 URL-friendly 的 Slug（支援中文），
**so that** 我不需要手動輸入 Slug，同時可以使用中文 URL。

**Acceptance Criteria:**
1. 建立 `Services/SlugService.cs` 提供 Slug 生成邏輯
2. Slug 生成規則：
   - 保留中文字符（UTF-8 編碼）
   - 保留英文字母（轉小寫）
   - 保留數字
   - 空格轉換為連字號 `-`
   - 移除特殊符號（除了連字號）
   - 多個連續連字號合併為單一連字號
3. 範例：
   - 「我的第一篇文章」→ `我的第一篇文章`
   - 「Hello World 123」→ `hello-world-123`
   - 「ASP.NET Core 教學」→ `aspnet-core-教學`
4. 在新增/編輯頁面，標題欄位失去焦點（blur）時，自動生成 Slug 並填入 Slug 欄位
5. 若 Slug 欄位已有內容（手動輸入），則不自動覆蓋
6. Slug 在資料庫層級需設定 Unique Index，防止重複
7. 儲存時若 Slug 重複，顯示錯誤訊息：「此 Slug 已被使用，請修改」

### Story 2.6: 刪除文章功能

**As a** 管理員，
**I want** 刪除不需要的文章，
**so that** 我可以移除過時或錯誤的內容。

**Acceptance Criteria:**
1. 在 `Pages/Admin/Posts/Index.cshtml` 的每一列文章提供「刪除」按鈕
2. 點擊「刪除」按鈕時，顯示確認對話框：「確定要刪除此文章嗎？此操作無法復原。」
3. 確認後，發送 DELETE 請求到後端
4. 後端刪除資料庫中的文章記錄
5. 刪除成功後，重新整理列表並顯示成功訊息：「文章已刪除」
6. 若文章 ID 不存在，返回 404 錯誤
7. 刪除已發布文章後，前台不再顯示該文章

---

## Epic 3: Public Front-End Display

**Epic Goal:**
實作前台公開展示功能，包含文章列表頁（含傳統分頁）、文章內容頁、Markdown 渲染為 HTML、程式碼語法高亮（Prism.js）、中文 Slug URL 路由，以及完整的 SEO Meta Tags。完成後，讀者可以瀏覽已發布的文章並享受優雅的閱讀體驗。

### Story 3.1: 前台文章列表頁（不含分頁）

**As a** 讀者，
**I want** 瀏覽所有已發布的文章清單，
**so that** 我可以選擇感興趣的文章閱讀。

**Acceptance Criteria:**
1. 建立 `Pages/Posts/Index.cshtml` 文章列表頁（URL: `/posts`）
2. 從資料庫讀取所有 `IsPublished = true` 的文章，依 `PublishedAt` 降序排序
3. 每篇文章顯示：
   - 標題（Title）- 可點擊連結到文章內容頁
   - 摘要（從 Content 截取前 150 字，移除 Markdown 語法）
   - 發布時間（PublishedAt，轉換為台灣時間 UTC+8，格式：`YYYY-MM-DD HH:mm:ss`）
4. 文章以卡片（Card）形式呈現（暫時使用基礎樣式，Epic 4 套用設計系統）
5. 點擊文章標題跳轉到 `/posts/{slug}`
6. 空列表顯示「尚無文章」
7. 暫時顯示所有文章（分頁功能在下一個 Story 實作）

### Story 3.2: 文章列表分頁導航

**As a** 讀者，
**I want** 文章列表支援分頁導航，
**so that** 頁面載入速度快且易於瀏覽大量文章。

**Acceptance Criteria:**
1. 文章列表每頁顯示 10 篇文章
2. URL 格式：
   - 第 1 頁：`/posts`
   - 第 2 頁：`/posts?page=2`
3. 實作分頁導航元件（在列表下方）：
   - 顯示：← 上一頁 [1] [2] [3] ... 下一頁 →
   - 當前頁碼以藍色高亮顯示
   - 第 1 頁時「上一頁」按鈕禁用
   - 最後一頁時「下一頁」按鈕禁用
4. 點擊頁碼正確跳轉到對應頁面
5. 若 `page` 參數超出範圍（< 1 或 > 總頁數），重導向到第 1 頁
6. SEO 友善：每個分頁頁面有獨特的 Canonical URL

### Story 3.3: Markdown 渲染服務

**As a** 系統，
**I want** 將文章的 Markdown 內容轉換為 HTML，
**so that** 文章可以在前台正確顯示格式化內容。

**Acceptance Criteria:**
1. 建立 `Services/MarkdownService.cs` 提供 Markdown → HTML 轉換
2. 使用 Markdig 處理 Markdown：
   - 支援 CommonMark 標準
   - 支援 GitHub Flavored Markdown (GFM)
   - 支援表格、任務清單、刪除線
3. 配置 Markdig Pipeline 啟用以下擴充：
   - 表格（Tables）
   - 任務清單（TaskLists）
   - 程式碼高亮（屬性標記，例如 `class="language-javascript"`）
4. 實作 XSS 防護：
   - 清理危險的 HTML 標籤（`<script>`, `<iframe>` 等）
   - 允許安全的 HTML 標籤（`<a>`, `<img>`, `<code>`, `<pre>` 等）
5. 建立單元測試驗證：
   - 基本 Markdown 語法正確轉換（標題、粗體、斜體、連結、圖片）
   - 程式碼區塊包含正確的語言標記（`class="language-{lang}"`）
   - XSS 攻擊向量被正確清理

### Story 3.4: 前台文章內容頁

**As a** 讀者，
**I want** 閱讀完整的文章內容，
**so that** 我可以獲取文章的詳細資訊。

**Acceptance Criteria:**
1. 建立 `Pages/Posts/Post.cshtml` 文章內容頁（URL: `/posts/{slug}`）
2. 根據 URL 的 `slug` 參數從資料庫查詢對應文章
3. 只顯示 `IsPublished = true` 的文章（草稿返回 404）
4. 頁面顯示以下內容：
   - 文章標題（`<h1>`）
   - 發布時間（格式：`YYYY-MM-DD HH:mm:ss`，轉換為台灣時間 UTC+8）
   - 文章內容（Markdown 渲染後的 HTML，使用 `MarkdownService`）
5. 文章內容區域最大寬度限制為 720px，置中顯示
6. 支援中文 Slug URL（UTF-8 編碼）
7. 若文章不存在或為草稿，顯示 404 錯誤頁面
8. 圖片（Markdown `![](url)`）正確顯示（支援外部連結與專案內靜態圖片）

### Story 3.5: 程式碼語法高亮（Prism.js）

**As a** 讀者，
**I want** 文章中的程式碼區塊有語法高亮，
**so that** 程式碼更易於閱讀與理解。

**Acceptance Criteria:**
1. 在 `_Layout.cshtml` 引入 Prism.js（從 CDN 或本地）
2. 引入 Prism.js CSS 主題（選擇與設計系統協調的主題）
3. 程式碼區塊使用 JetBrains Mono 字型
4. Prism.js 自動偵測 Markdig 生成的 `<pre><code class="language-{lang}">` 結構
5. 支援常見程式語言：JavaScript, TypeScript, Python, C#, Java, Go, Rust, HTML, CSS, Bash
6. 程式碼區塊包含行號（選用，可在 Epic 4 決定）
7. 驗證語法高亮正確運作：
   - 建立測試文章包含多種語言的程式碼區塊
   - 確認每種語言的關鍵字、字串、註解正確高亮

### Story 3.6: SEO Meta Tags 實作

**As a** 部落格擁有者，
**I want** 每個頁面都有正確的 SEO Meta Tags，
**so that** 搜尋引擎可以正確索引內容，社群分享時有良好的預覽效果。

**Acceptance Criteria:**
1. 建立 `Services/SeoService.cs` 或 Base Page Model 管理 SEO 資訊
2. 每個頁面（首頁、文章列表、文章內容）需設定：
   - `<title>` - 頁面獨特標題
   - `<meta name="description">` - 頁面描述
   - `<link rel="canonical">` - Canonical URL
3. 文章內容頁額外包含：
   - **Open Graph Tags:**
     - `og:title` - 文章標題
     - `og:description` - 文章摘要（前 150 字）
     - `og:type` - `article`
     - `og:url` - 文章 URL
     - `og:image` - 文章首圖（暫時使用預設圖片，Phase 2 支援自訂）
   - **Twitter Card Tags:**
     - `twitter:card` - `summary_large_image`
     - `twitter:title` - 文章標題
     - `twitter:description` - 文章摘要
     - `twitter:image` - 文章首圖
4. 範例標題格式：
   - 首頁：「.NET 個人部落格系統」
   - 文章列表：「文章列表 | .NET 個人部落格系統」
   - 文章內容：「{文章標題} | .NET 個人部落格系統」
5. 使用 Facebook Sharing Debugger 驗證 Open Graph Tags
6. 使用 Twitter Card Validator 驗證 Twitter Card Tags
7. 語意化 HTML：使用 `<article>`, `<header>`, `<main>`, `<nav>` 標籤

---

## Epic 4: Design System & Responsive Polish

**Epic Goal:**
實作完整的設計系統規範，包含 Tailwind CSS 整合、Design Tokens 配置、響應式佈局、UI 元件樣式（卡片、按鈕、輸入框、導覽列）、字型載入優化、頁面過渡動畫，以及效能調校達到 Lighthouse Performance > 90。完成後，應用呈現精美的白/藍色設計風格與卓越的使用者體驗。

### Story 4.1: Tailwind CSS 整合與設定

**As a** 開發者，
**I want** 整合 Tailwind CSS 並配置 Design Tokens，
**so that** 可以使用 Utility Classes 快速實作設計系統。

**Acceptance Criteria:**
1. 安裝 Node.js 套件：`npm install -D tailwindcss postcss autoprefixer`
2. 執行 `npx tailwindcss init` 建立 `tailwind.config.js`
3. 配置 `tailwind.config.js` 套用 Design Tokens：
   - **Colors:** Azure Blue 系統（`primary`, `primary-dark`, `primary-light`, `primary-pale`）與灰階
   - **Font Family:** `sans: ['Noto Sans TC', ...]`, `mono: ['JetBrains Mono', ...]`
   - **Font Size:** 自訂字型尺寸（xs, sm, base, lg, xl, 2xl, 3xl, 4xl, 5xl）
   - **Spacing:** 4px 基準的間距系統（1, 2, 3, 4, 5, 6, 8, 10, 12, 16, 20, 24）
   - **Border Radius:** `sm: 4px`, `md: 6px`, `lg: 8px`, `full: 9999px`
   - **Box Shadow:** 微陰影（xs, sm, md, lg, focus）
4. 建立 `wwwroot/css/site.css` 引入 Tailwind：
   ```css
   @tailwind base;
   @tailwind components;
   @tailwind utilities;
   ```
5. 設定 PostCSS 處理流程（`postcss.config.js`）
6. 在 `package.json` 新增 build script：`"build:css": "tailwindcss -i ./wwwroot/css/site.css -o ./wwwroot/css/output.css --minify"`
7. 在 `_Layout.cshtml` 引入編譯後的 CSS
8. 驗證 Tailwind Classes 可正常套用（例如：`bg-primary`, `text-white`, `p-4`）

### Story 4.2: 字型載入與優化

**As a** 讀者，
**I want** 頁面載入時字型快速顯示且無閃爍，
**so that** 閱讀體驗流暢。

**Acceptance Criteria:**
1. 在 `_Layout.cshtml` 的 `<head>` 中載入 Google Fonts：
   - Noto Sans TC（weights: 400, 500, 600, 700）
   - JetBrains Mono（weights: 400, 500, 700）
2. 使用 `<link rel="preconnect">` 優化 Google Fonts 載入：
   ```html
   <link rel="preconnect" href="https://fonts.googleapis.com">
   <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
   ```
3. 字型 CSS 使用 `font-display: swap` 避免 FOIT（Flash of Invisible Text）
4. 驗證字型正確載入：
   - 內文使用 Noto Sans TC
   - 程式碼區塊使用 JetBrains Mono
5. Lighthouse Performance 不因字型載入影響評分

### Story 4.3: 導覽列與基礎佈局

**As a** 使用者，
**I want** 網站有清楚的導覽列與一致的佈局，
**so that** 我可以輕鬆瀏覽不同頁面。

**Acceptance Criteria:**
1. 在 `_Layout.cshtml` 實作全站導覽列（固定在頂部，高度 64px）：
   - Logo/網站名稱（左側）
   - 導覽連結（右側）：首頁、文章列表
   - 管理員登入後顯示「後台管理」連結與「登出」按鈕
2. 導覽列樣式：
   - 背景：白色（`bg-white`）
   - 陰影：`shadow-sm`
   - 文字顏色：深灰（`text-gray-800`）
   - 連結 Hover：文字顏色變為 Azure Blue（`hover:text-primary`）
3. 實作 Footer（頁尾）：
   - 顯示版權資訊：「© 2025 .NET 個人部落格系統」
   - 背景：淺灰（`bg-gray-50`）
   - 文字顏色：次要灰（`text-gray-600`）
4. 響應式導覽（手機版）：
   - 使用 Hamburger Menu（漢堡選單）
   - 點擊展開側邊選單（使用 Alpine.js 或 vanilla JS）
5. 所有頁面使用一致的 Layout（導覽列 + 內容區 + Footer）

### Story 4.4: 卡片元件樣式（文章列表）

**As a** 讀者，
**I want** 文章列表以精美的卡片形式呈現，
**so that** 視覺呈現吸引人且易於瀏覽。

**Acceptance Criteria:**
1. 在文章列表頁（`Pages/Posts/Index.cshtml`）套用卡片樣式：
   - 背景：`bg-gray-100`
   - 圓角：`rounded-md` (6px)
   - 陰影：`shadow-sm`
   - Padding：`p-6` (24px)
2. Hover 效果：
   - 陰影變為 `shadow-md`
   - 微上移：`transform: translateY(-2px)`
   - 過渡動畫：`transition-all duration-250 ease-in-out`
3. 卡片內容佈局：
   - 標題：`text-2xl font-semibold text-gray-800 mb-2`
   - 摘要：`text-base text-gray-600 mb-4 leading-relaxed`
   - 發布時間：`text-sm text-gray-500`
4. 卡片之間間距：`space-y-4`
5. 響應式佈局：
   - 桌面：單欄佈局，最大寬度 960px 置中
   - 手機：單欄佈局，側邊留白 16px

### Story 4.5: 按鈕與表單元件樣式

**As a** 管理員，
**I want** 後台表單與按鈕有統一的樣式，
**so that** 介面一致且操作直觀。

**Acceptance Criteria:**
1. 定義主按鈕（Primary Button）樣式：
   - 背景：`bg-primary`
   - 文字：`text-white`
   - Hover：`hover:bg-primary-dark`
   - 圓角：`rounded` (4px)
   - Padding：`px-4 py-2` (水平 16px, 垂直 8px)
   - 過渡：`transition-colors duration-150`
2. 定義次按鈕（Secondary Button）樣式：
   - 邊框：`border border-primary`
   - 文字：`text-primary`
   - Hover：`hover:bg-primary-pale`
3. 定義輸入框（Input）樣式：
   - 邊框：`border border-gray-300`
   - Focus 邊框：`focus:border-primary focus:ring focus:ring-primary focus:ring-opacity-10`
   - 圓角：`rounded` (4px)
   - Padding：`px-3 py-2` (12px)
4. 套用到所有後台表單（新增/編輯文章頁面）
5. 驗證鍵盤 Tab 導航 Focus 狀態清晰可見

### Story 4.6: 文章內容頁排版與樣式

**As a** 讀者，
**I want** 文章內容頁有優雅的排版與適當的行距，
**so that** 長時間閱讀也不疲勞。

**Acceptance Criteria:**
1. 文章內容區域（`<article>`）樣式：
   - 最大寬度：720px
   - 置中顯示：`mx-auto`
   - 行距：`leading-relaxed` (1.75)
   - 段落間距：`space-y-6` (24px)
2. 標題樣式（`<h1>` - `<h6>`）：
   - `<h1>`: `text-4xl font-bold text-gray-900 mb-4`
   - `<h2>`: `text-3xl font-semibold text-gray-800 mt-8 mb-4`
   - `<h3>`: `text-2xl font-semibold text-gray-800 mt-6 mb-3`
3. 連結樣式（`<a>`）：
   - 顏色：`text-primary`
   - Hover：`hover:underline`
4. 程式碼區塊（`<pre><code>`）：
   - 背景：`bg-gray-900`
   - 文字顏色：`text-gray-100`
   - 圓角：`rounded-md`
   - Padding：`p-4`
   - 字型：`font-mono` (JetBrains Mono)
5. 行內程式碼（`<code>`）：
   - 背景：`bg-gray-200`
   - 文字顏色：`text-gray-800`
   - Padding：`px-1.5 py-0.5`
   - 圓角：`rounded`
6. 圖片（`<img>`）：
   - 最大寬度：`max-w-full`
   - 圓角：`rounded-lg`
   - 陰影：`shadow-md`
7. 發布時間樣式：`text-sm text-gray-500 mb-8`

### Story 4.7: 響應式設計測試與調整

**As a** 使用者，
**I want** 網站在各種裝置上都有良好的顯示效果，
**so that** 無論使用桌面、平板或手機都能順暢操作。

**Acceptance Criteria:**
1. 測試三種裝置尺寸：
   - 桌面（1920x1080）
   - 平板（768x1024）
   - 手機（375x667, iPhone SE）
2. 驗證以下頁面響應式表現：
   - 首頁
   - 文章列表頁
   - 文章內容頁
   - 後台文章管理列表
   - 後台新增/編輯文章頁
3. 手機版調整：
   - 導覽列使用 Hamburger Menu
   - 文章卡片寬度撐滿（扣除 16px 側邊留白）
   - 文章內容區側邊留白減少為 16px
   - 按鈕與輸入框尺寸適合觸控操作（最小高度 44px）
4. 平板版調整：
   - 文章列表可選單欄或雙欄佈局（視設計決定）
   - 文章內容區最大寬度 720px 保持不變
5. 所有互動元件（按鈕、連結、輸入框）在觸控裝置上操作流暢
6. 無水平滾動條（所有內容適應螢幕寬度）

### Story 4.8: 效能優化與 Lighthouse 測試

**As a** 部落格擁有者，
**I want** 網站達到卓越的效能指標，
**so that** 讀者載入速度快，且搜尋引擎排名更高。

**Acceptance Criteria:**
1. 執行 Lighthouse 測試（以文章內容頁為主要測試頁面）
2. 達成以下目標：
   - **Performance Score > 90**
   - **Accessibility Score > 90**
   - **Best Practices Score > 90**
   - **SEO Score > 95**
3. 效能優化措施：
   - CSS/JS 檔案最小化（Minify）
   - Tailwind CSS Purge（移除未使用的 Classes）
   - 圖片使用 WebP 格式（若專案內有靜態圖片）
   - 啟用 Response Compression Middleware（Gzip/Brotli）
   - 設定靜態資源 Cache-Control Headers（1 年）
4. 驗證 Core Web Vitals：
   - **LCP (Largest Contentful Paint) < 2.5s**
   - **FID (First Input Delay) < 100ms**
   - **CLS (Cumulative Layout Shift) < 0.1**
5. 使用 Chrome DevTools Network Tab 驗證：
   - 首頁載入時間 < 2 秒
   - 文章內容頁載入時間 < 2 秒
6. 若未達標，分析瓶頸並調整（字型載入、圖片大小、JS 執行時間）

---

## Checklist Results Report

_（本段將在 PM Checklist 執行完成後填入結果報告）_

---

## Next Steps

### UX Expert Prompt

由於本專案的 UI Design Goals 已在 Project Brief 中完整定義（包含詳細的 Design System Specifications），且技術架構選擇 ASP.NET Core Razor Pages（伺服器端渲染），**暫不需要啟動 UX Expert 建立獨立的前端規格文件**。

設計系統規範已直接整合到 PRD 與 Epic 4 的 User Stories 中，Architect 可直接參考這些資訊進行架構設計。

若未來需要更複雜的互動設計（如前後端分離的 SPA）或詳細的 Wireframes/Mockups，再考慮啟動 UX Expert。

### Architect Prompt

請以 **Architect** 身份，基於本 PRD 建立完整的 **Architecture Document（架構文件）**。

**輸入文件：**
- `docs/prd.md`（本文件）
- `docs/brief.md`（Project Brief，包含完整的 Design System Specifications）

**請涵蓋以下架構領域：**

1. **System Architecture Overview**
   - 高階系統架構圖
   - 技術堆疊決策與理由
   - 部署架構（容器化 + PostgreSQL）

2. **Application Architecture**
   - ASP.NET Core Razor Pages 專案結構
   - 分層架構設計（Presentation, Business, Data）
   - 資料夾組織與命名慣例

3. **Data Architecture**
   - 完整的資料庫 Schema（Posts, Admins）
   - Entity Relationships（目前僅兩個獨立表，未來擴展需考慮關聯）
   - EF Core Configuration（Fluent API, Migrations）

4. **Integration Architecture**
   - Google OAuth 2.0 整合流程圖
   - 環境變數白名單驗證機制
   - 第三方服務（Google Fonts, Prism.js CDN）

5. **Security Architecture**
   - Authentication & Authorization 流程
   - HTTPS 強制執行
   - CSRF/XSS 防護策略
   - 敏感資訊管理（環境變數、Secrets）

6. **Deployment Architecture**
   - Dockerfile 多階段建置設計
   - docker-compose.yml 服務編排
   - 容器網路與 Volume 管理
   - 生產環境部署建議（平台選擇：Fly.io, Railway, Azure 等）

7. **Testing Strategy**
   - 單元測試範圍（Services 層）
   - 整合測試範圍（Razor Pages 端到端）
   - 測試框架與工具（xUnit, FluentAssertions）

8. **Performance & Scalability**
   - 效能優化策略（Gzip, CSS Purge, Response Caching）
   - 資料庫索引設計（Slug Unique Index, PublishedAt Index）
   - 未來擴展性考量（分類、標籤、搜尋）

9. **Code Standards & Conventions**
   - C# 命名慣例
   - Razor Pages 組織規範
   - Git Commit Message 規範

**請確保架構文件：**
- 與 PRD 的所有功能需求對齊
- 與 Project Brief 的設計系統規範對齊
- 提供清楚的實作指導給 Dev Agent
- 包含架構圖與資料流程圖（使用 Mermaid 或其他格式）

**輸出檔案：** `docs/architecture.md`
