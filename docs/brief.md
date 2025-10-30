# Project Brief: .NET 個人部落格系統

## Executive Summary

一個以設計為先的 .NET 個人部落格平台，提供優雅的閱讀體驗與直觀的內容管理功能。本專案採用 Google OAuth 進行安全的後台認證，僅允許指定的管理員存取內容管理系統。

**主要解決的問題：** 現有部落格平台往往設計過時、載入緩慢，或需要複雜的權限管理系統，不適合個人部落格使用。

**目標市場：** 重視內容品質與閱讀體驗的個人部落客、技術寫作者。

**關鍵價值主張：** 結合精美的 UI/UX 設計、簡單的單一管理員模式，以及現代 .NET 技術堆疊，打造快速、安全、美觀的個人部落格平台。

---

## Problem Statement

### 現況與痛點

許多個人部落客面臨以下困境：
- **設計過時：** 現有免費部落格平台（如 Blogger、WordPress.com）的預設主題設計陳舊，自訂困難
- **效能問題：** 許多平台載入緩慢，影響讀者體驗與 SEO 排名
- **權限複雜：** 多數 CMS 系統設計給團隊使用，個人部落格不需要複雜的角色權限管理
- **依賴第三方：** 使用託管平台意味著受限於平台政策，缺乏完整控制權

### 問題的影響

- 讀者因載入速度慢或設計不佳而離開，降低內容影響力
- 部落客花費過多時間在平台設定而非內容創作
- 缺乏品牌一致性，無法打造獨特的視覺識別

### 現有解決方案的不足

- **WordPress 自架：** 功能強大但過於複雜，需要維護外掛、處理安全更新
- **靜態網站產生器：** 缺乏友善的內容管理介面，需要技術背景
- **Medium/Substack：** 無法完全自訂設計，受限於平台規則

### 為何現在解決很重要

隨著內容創作競爭加劇，提供優質的閱讀體驗已成為差異化關鍵。現代讀者期待快速、美觀、適合行動裝置的閱讀介面。

---

## Proposed Solution

### 核心概念與方法

打造一個輕量級的 .NET 部落格系統，專注於：
1. **設計優先：** 以精心設計的白色/藍色配色系統，提供清新、專業的視覺體驗
2. **簡化認證：** 使用 Google OAuth + 環境變數白名單，無需複雜的用戶管理系統
3. **核心功能：** 專注於文章的 CRUD 操作與發布流程，避免功能膨脹
4. **效能優化：** 使用現代 .NET 技術堆疊，確保快速載入與良好的 SEO 表現

### 關鍵差異

- **專為個人設計：** 不是縮小版的企業 CMS，而是從個人需求出發的專用系統
- **設計即核心功能：** UI/UX 不是附加價值，而是產品的核心競爭力
- **極簡認證：** 環境變數白名單機制，避免用戶管理的複雜性

### 成功因素

- 利用 .NET 的高效能與成熟生態系
- 清晰的設計系統（白/藍配色）確保視覺一致性
- 簡化的功能範圍讓開發與維護更容易

### 產品願景

成為技術部落客與內容創作者的首選自架部落格解決方案，以「開箱即美」的體驗，讓創作者專注於內容本身。

---

## Target Users

### Primary User Segment: 個人技術部落客

**人口統計特徵：**
- 軟體工程師、技術寫作者
- 具備基本的開發與部署能力
- 重視個人品牌與內容品質

**目前行為與工作流程：**
- 使用 Markdown 或線上編輯器撰寫文章
- 可能已在使用 Medium、Dev.to 等平台
- 希望擁有自己的網域與完整控制權

**具體需求與痛點：**
- 需要簡單但專業的內容管理介面
- 希望部落格設計能反映個人品牌
- 不想花時間處理複雜的系統維護

**目標：**
- 建立個人技術品牌
- 分享知識與經驗
- 提供優質的讀者體驗

### Secondary User Segment: 部落格讀者

**人口統計特徵：**
- 對技術內容有興趣的專業人士或學習者
- 跨裝置閱讀（桌面、平板、手機）

**具體需求：**
- 快速載入與流暢的閱讀體驗
- 清晰的排版與視覺層級
- 容易找到相關文章

**目標：**
- 快速獲取資訊
- 舒適的閱讀體驗
- 探索更多相關內容

---

## Goals & Success Metrics

### Business Objectives

- **6 個月內完成 MVP 開發並正式上線**
- **第一年內至少支援 1 位作者穩定使用（概念驗證）**
- **頁面載入時間 < 2 秒（Lighthouse Performance Score > 90）**
- **達成 100% 行動裝置適配（Responsive Design）**

### User Success Metrics

- **內容管理效率：** 從登入到發布新文章 < 5 分鐘
- **閱讀體驗：** 讀者平均停留時間 > 2 分鐘
- **易用性：** 管理員無需查閱文件即可完成基本操作
- **設計滿意度：** UI/UX 視覺品質獲得正面回饋

### Key Performance Indicators (KPIs)

- **頁面載入速度：** First Contentful Paint < 1.5 秒
- **SEO 表現：** Google Lighthouse SEO Score > 95
- **可用性：** 零重大安全漏洞（Security Audit Pass）
- **部署成功率：** 環境設定後首次部署成功率 > 90%

---

## MVP Scope

### Core Features (Must Have)

- **Google OAuth 認證：** 整合 Google OAuth 2.0，透過環境變數（如 `ALLOWED_ADMIN_EMAIL`）控制後台存取權限
  - 只允許白名單內的 Google 帳號登入後台
  - 其他人只能閱讀前台內容

- **文章 CRUD 功能：**
  - Markdown 編輯器（**EasyMDE**）
  - 新增、編輯、刪除文章
  - **中文 Slug 支援：** 自動從標題生成 Slug（支援中文），可手動修改
  - 後端使用 Markdig 處理 Markdown → HTML
  - 程式碼區塊語法高亮（**Prism.js**）

- **發布狀態管理：**
  - 草稿/已發布狀態切換
  - 發布時間記錄（台灣時區 UTC+8）

- **圖片處理（MVP）：**
  - **僅支援外部圖片連結**（Markdown 標準語法：`![](url)`）
  - 可使用免費圖床（Imgur、Cloudinary）或專案內靜態圖片
  - **Phase 2 再實作完整上傳功能**

- **前台文章列表頁：**
  - 顯示已發布文章清單
  - **傳統分頁導航**（每頁 10 篇）
  - 顯示文章標題、摘要、發布時間
  - URL: `/posts` (第1頁) 或 `/posts?page=2`

- **前台文章內容頁：**
  - 優雅的文章閱讀體驗（最大寬度 720px）
  - 程式碼高亮（使用 JetBrains Mono 字型）
  - 發布時間顯示（格式：YYYY-MM-DD HH:mm:ss）
  - URL: `/posts/{slug}`（支援中文 Slug）

- **SEO 基礎功能（MVP 包含）：**
  - **基本 Meta Tags：** 每個頁面獨特的 title 和 description
  - **Open Graph Tags：** Facebook、LinkedIn 分享預覽
  - **Twitter Card Tags：** Twitter 分享預覽
  - **Canonical URL：** 避免重複內容問題
  - 語意化 HTML（`<article>`, `<header>`, `<main>`, `<nav>`）

- **響應式設計：** 完整支援桌面、平板、手機裝置

- **白/藍色設計系統：**
  - 實作統一的設計語言（Azure Blue #0078D4 + 白色）
  - 思源黑體（Noto Sans TC）+ JetBrains Mono
  - Tailwind CSS 實作，套用完整 Design Tokens

### Out of Scope for MVP

- **圖片上傳系統**（MVP 只用外部連結，Phase 2 實作上傳、縮圖、管理）
- **文章分類/標籤系統**
- **搜尋功能**（全站搜尋或文章內搜尋）
- **留言功能**（可考慮整合第三方如 Disqus、giscus）
- **多作者支援**（MVP 只有單一管理員）
- **RSS Feed**（Phase 2）
- **社群分享按鈕**（雖然有 Open Graph，但不做分享按鈕）
- **SEO 進階功能：**
  - Sitemap.xml 自動生成（Phase 2）
  - Structured Data (JSON-LD)（Phase 2）
  - 自訂每篇文章的 meta tags（Phase 2）
- **文章版本歷史**（Git-like 版本控制）
- **Analytics 整合**（Google Analytics 或 Plausible）
- **無限滾動**（使用傳統分頁）
- **深色模式**（Phase 2）
- **閱讀進度條**（Phase 2）
- **預估閱讀時間**（Phase 2）
- **相關文章推薦**（Phase 2）

### MVP Success Criteria

**MVP 成功的定義：**

1. **認證功能：**
   - 管理員可以透過 Google OAuth 順利登入後台
   - 環境變數白名單正確運作（非白名單無法登入）
   - 未登入使用者可正常瀏覽前台

2. **內容管理：**
   - 可以建立、編輯、刪除、發布至少 10 篇文章
   - Markdown 編輯器正常運作（工具列、預覽、快捷鍵）
   - 中文 Slug 自動生成並可手動修改
   - 草稿與已發布狀態切換正常

3. **前台展示：**
   - 文章列表正確顯示（分頁導航正常運作）
   - 文章內容頁正確渲染（Markdown → HTML）
   - 程式碼高亮正常顯示
   - 中文 URL 在瀏覽器中正確顯示

4. **設計系統：**
   - 在三種裝置（桌面/平板/手機）上視覺呈現正確且美觀
   - 設計系統一致性（顏色、字型、間距、圓角、陰影）
   - Tailwind CSS Design Tokens 正確套用

5. **SEO 驗證：**
   - 每個頁面都有正確的 title 和 description
   - Open Graph Tags 正確（用 Facebook Debugger 驗證）
   - Lighthouse 各項分數達標：
     - Performance > 90
     - Accessibility > 90
     - Best Practices > 90
     - SEO > 95

6. **容器化部署：**
   - Podman/Docker 環境本地運行正常
   - PostgreSQL 資料持久化正常
   - 環境變數配置正確

---

## Post-MVP Vision

### Phase 2 Features

**內容管理增強：**
- 文章分類與標籤系統
- 圖片上傳與管理（整合 CDN）
- 文章草稿自動儲存
- SEO meta tags 自訂介面

**讀者體驗優化：**
- 全站搜尋功能
- 相關文章推薦
- RSS Feed 訂閱
- 深色模式支援

### Long-term Vision

**1-2 年願景：**

成為一個可擴展的內容平台框架，不僅限於部落格，還能支援：
- 作品集展示（Portfolio）
- 技術文件站（Documentation Site）
- 電子報發送（Newsletter Integration）
- 多語言內容支援

同時保持核心的簡潔性與設計品質，可能發展成開源專案，讓更多創作者受益。

### Expansion Opportunities

- **主題系統：** 允許用戶選擇或自訂配色方案
- **外掛機制：** 支援第三方功能擴充
- **API 開放：** 提供 REST API 供行動應用或其他前端使用
- **多部落格實例：** 單一部署支援多個獨立部落格
- **社群功能：** 讀者註冊、收藏文章、訂閱通知

---

## Technical Considerations

### Design System Specifications

#### Color Palette

**Primary Colors:**
- **主色調 - 白色系統**
  - `--color-white`: `#FFFFFF` - 主背景
  - `--color-gray-50`: `#F9FAFB` - 次要背景
  - `--color-gray-100`: `#F3F4F6` - 卡片背景
  - `--color-gray-200`: `#E5E7EB` - 邊框、分隔線
  - `--color-gray-300`: `#D1D5DB` - 次要邊框

- **輔助色 - Azure Blue 系統**
  - `--color-primary`: `#0078D4` - 主要藍色（連結、按鈕、重點）
  - `--color-primary-dark`: `#005A9E` - Hover 狀態
  - `--color-primary-light`: `#50A3E0` - 淺色變體
  - `--color-primary-pale`: `#E6F2FA` - 背景高亮、標籤

**Text Colors:**
- `--color-text-primary`: `#1F2937` - 主要文字（深灰）
- `--color-text-secondary`: `#6B7280` - 次要文字
- `--color-text-tertiary`: `#9CA3AF` - 輔助文字、說明文字

**Semantic Colors:**
- `--color-success`: `#10B981` - 成功狀態
- `--color-warning`: `#F59E0B` - 警告
- `--color-error`: `#EF4444` - 錯誤
- `--color-info`: `#0078D4` - 資訊（使用主色）

#### Typography

**Font Families:**
- **內文/介面：** 'Noto Sans TC', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif
- **程式碼：** 'JetBrains Mono', 'Fira Code', 'Consolas', monospace

**Font Sizes (Fluid Typography):**
- `--text-xs`: 12px (0.75rem) - 輔助說明
- `--text-sm`: 14px (0.875rem) - 小字、標籤
- `--text-base`: 16px (1rem) - 基準文字
- `--text-lg`: 18px (1.125rem) - 引言、重點
- `--text-xl`: 20px (1.25rem) - 小標題
- `--text-2xl`: 24px (1.5rem) - 副標題
- `--text-3xl`: 30px (1.875rem) - 主標題
- `--text-4xl`: 36px (2.25rem) - 大標題
- `--text-5xl`: 48px (3rem) - Hero 標題

**Font Weights:**
- `--font-normal`: 400
- `--font-medium`: 500
- `--font-semibold`: 600
- `--font-bold`: 700

**Line Heights:**
- `--leading-tight`: 1.25 - 標題
- `--leading-normal`: 1.5 - 一般文字
- `--leading-relaxed`: 1.75 - 長文閱讀

#### Spacing System (基於 4px 基準)

- `--space-1`: 4px
- `--space-2`: 8px
- `--space-3`: 12px
- `--space-4`: 16px
- `--space-5`: 20px
- `--space-6`: 24px
- `--space-8`: 32px
- `--space-10`: 40px
- `--space-12`: 48px
- `--space-16`: 64px
- `--space-20`: 80px
- `--space-24`: 96px

#### Border Radius

- `--radius-sm`: 4px - 小元件（按鈕、輸入框）
- `--radius-md`: 6px - 標準卡片
- `--radius-lg`: 8px - 大卡片、模態框
- `--radius-full`: 9999px - 圓形頭像、標籤

#### Shadows (微陰影設計)

- `--shadow-xs`: 0 1px 2px 0 rgba(0, 0, 0, 0.05) - 微妙元件
- `--shadow-sm`: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06) - 卡片
- `--shadow-md`: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06) - Hover 卡片
- `--shadow-lg`: 0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05) - 模態框、下拉選單
- `--shadow-focus`: 0 0 0 3px rgba(0, 120, 212, 0.1) - Focus 狀態

#### Animation & Transitions

**Duration:**
- `--duration-fast`: 150ms - 微互動（Hover）
- `--duration-base`: 250ms - 標準過渡
- `--duration-slow`: 350ms - 複雜動畫

**Easing:**
- `--ease-in-out`: cubic-bezier(0.4, 0, 0.2, 1) - 標準
- `--ease-out`: cubic-bezier(0, 0, 0.2, 1) - 進入動畫
- `--ease-in`: cubic-bezier(0.4, 0, 1, 1) - 退出動畫

**Common Transitions:**
```css
transition: all var(--duration-base) var(--ease-in-out);
```

#### Component Patterns

**卡片 (Card):**
- 背景：`--color-gray-100`
- 圓角：`--radius-md` (6px)
- 陰影：`--shadow-sm`
- Hover：`--shadow-md` + 微上移（transform: translateY(-2px)）
- Padding：`--space-6` (24px)

**按鈕 (Button):**
- 主按鈕：背景 `--color-primary`，文字白色，Hover 變為 `--color-primary-dark`
- 次按鈕：邊框 `--color-primary`，文字 `--color-primary`，Hover 背景 `--color-primary-pale`
- 圓角：`--radius-sm` (4px)
- Padding：垂直 `--space-2` (8px)，水平 `--space-4` (16px)

**輸入框 (Input):**
- 邊框：`--color-gray-300`
- Focus 邊框：`--color-primary`
- Focus Shadow：`--shadow-focus`
- 圓角：`--radius-sm` (4px)
- Padding：`--space-3` (12px)

#### Responsive Breakpoints

- `--screen-sm`: 640px - 手機橫向
- `--screen-md`: 768px - 平板直向
- `--screen-lg`: 1024px - 平板橫向 / 小筆電
- `--screen-xl`: 1280px - 桌面
- `--screen-2xl`: 1536px - 大桌面

#### Content Layout

**文章閱讀區：**
- 最大寬度：720px（適合閱讀的行寬）
- 行距：`--leading-relaxed` (1.75)
- 段落間距：`--space-6` (24px)

**導覽列高度：** 64px

**側邊留白：**
- Mobile：`--space-4` (16px)
- Tablet：`--space-6` (24px)
- Desktop：`--space-8` (32px)

#### Design Principles

1. **清晰的視覺層級：** 使用大小、粗細、顏色建立層級
2. **充足的留白：** 避免擁擠感，提升可讀性
3. **一致的間距：** 使用 4px 基準的間距系統
4. **微妙的互動回饋：** 流暢動畫 + 微陰影變化
5. **可訪問性：** 確保對比度符合 WCAG AA 標準（4.5:1）

---

### Platform Requirements

- **Target Platforms:** Web 應用程式（瀏覽器存取）
- **Browser/OS Support:**
  - 桌面：Chrome, Firefox, Safari, Edge（最新兩個版本）
  - 行動：iOS Safari, Chrome Mobile（最新版本）
- **Performance Requirements:**
  - 首屏載入 < 2 秒
  - Time to Interactive < 3.5 秒
  - Lighthouse Performance Score > 90

### Technology Preferences

- **Frontend:**
  - **✅ ASP.NET Core Razor Pages**（已確定）
  - **選擇理由：**
    - 完美的 SEO 支援（伺服器端渲染完整 HTML）
    - 成熟穩定的技術（自 2017 年，社群資源豐富）
    - 開發速度快，學習曲線平緩
    - 非常適合內容驅動的部落格網站
    - 單一容器部署，架構簡潔
  - **UI 實作：**
    - Tailwind CSS 實作設計系統（自訂配置套用 Design Tokens）
    - Alpine.js 或少量 vanilla JS 處理輕量互動（下拉選單、模態框）
    - **✅ Markdown 編輯器：** EasyMDE（JavaScript）
      - SimpleMDE 已停止維護（2018），EasyMDE 是活躍 fork
      - 輕量、易整合、支援工具列與快捷鍵
      - 即時預覽功能
      - 可自訂樣式以符合設計系統
      - 持續維護，安全性更新及時

- **Backend:**
  - ASP.NET Core 8.0（LTS 版本）
  - Entity Framework Core for ORM
  - Google OAuth 2.0 Authentication

  - **✅ Markdown 處理：** Markdig（C# Markdown 處理器）
    - 支援 CommonMark + GFM（GitHub Flavored Markdown）
    - 擴充功能：程式碼高亮、表格、任務清單
    - 前端渲染已處理的 HTML（安全、快速）

  - **✅ 時區處理：**
    - 資料庫儲存 UTC 時間
    - 顯示時轉換為台灣時間（UTC+8）
    - 格式：`YYYY-MM-DD HH:mm:ss`
    - 相對時間（可選）：發布 3 小時內顯示「3 小時前」

  - **✅ 分頁配置：**
    - 每頁 10 篇文章
    - URL 格式：`/posts` (第1頁) 或 `/posts?page=2`
    - 分頁導航：← 上一頁 [1] [2] [3] ... 下一頁 →
    - 傳統分頁（非無限滾動）

  - **✅ URL Routing：**
    - 文章列表：`/posts` 或 `/posts?page={n}`
    - 文章內容：`/posts/{slug}`（支援中文 Slug）
    - 後台管理：`/admin/posts`、`/admin/posts/create`、`/admin/posts/edit/{id}`

- **Database:**
  - **✅ PostgreSQL**（已確定）
  - **選擇理由：**
    - 功能完整、成熟穩定的關係型資料庫
    - 優秀的效能與可擴展性
    - 容器化部署簡單（官方 Docker 映像）
    - 開發與生產環境一致（避免 SQLite → PostgreSQL 遷移問題）
    - 支援進階功能（全文檢索、JSON 欄位等，為未來擴展預留空間）
  - **資料模型：**
    - Posts（文章）- id, title, slug, content, published_at, created_at, updated_at
    - Admins（管理員白名單）- id, email, name, created_at
    - Categories（分類，Phase 2）
    - Media（媒體檔案，Phase 2）

- **Hosting/Infrastructure:**
  - **容器化部署：** 使用 Docker/Podman 容器進行開發與部署
  - **開發環境：** 本機使用 Podman（與 Docker 相容）
  - **生產環境：** 支援任何容器平台（Docker、Podman、Kubernetes、Azure Container Apps 等）
  - 考慮靜態資源 CDN（Azure CDN / Cloudflare）

- **Development Environment:**
  - 基於容器的開發環境（Dev Container / Podman）
  - Docker Compose / Podman Compose 用於本地服務編排（應用 + 資料庫）
  - 確保 Dockerfile 與 Podman 完全相容（避免 Docker 專屬語法）

### Architecture Considerations

- **Repository Structure:**
  - **單一 Monorepo**（Razor Pages 單體應用）
  - **清晰的分層架構：**
    - `Pages/` - Razor Pages（前台 + 後台）
    - `Services/` - Business Logic
    - `Data/` - Entity Framework Core Models & DbContext
    - `wwwroot/` - 靜態資源（CSS, JS, 圖片）
  - 包含 Dockerfile、docker-compose.yml（與 Podman 相容）
  - `.editorconfig`、`.gitignore` 等開發配置

- **Container Architecture:**
  - **✅ 基底映像：** Debian（開發與生產環境一致）
    - 使用：`mcr.microsoft.com/dotnet/aspnet:8.0`（Debian 基底）
    - 選擇理由：穩定、相容性好、問題少、除錯容易
    - 映像檔大小：約 230-250MB（可接受，不需過早優化）
    - 未來若需要可輕鬆改為 Alpine（只改 Dockerfile 一行）

  - **多階段建置（Multi-stage Build）：**
    - Stage 1: Build（使用 `mcr.microsoft.com/dotnet/sdk:8.0` 建置應用）
    - Stage 2: Runtime（使用 `mcr.microsoft.com/dotnet/aspnet:8.0` 執行）
    - 優化映像檔大小

  - **服務編排：** 應用容器 + PostgreSQL 容器的本地開發環境

  - **Volume 管理：**
    - 資料庫持久化（postgres-data volume）
    - 開發時程式碼同步（Hot Reload）

  - **網路設定：** 容器間通訊與外部存取設定

  - **環境變數注入：** 透過 .env 檔案或容器編排工具管理

- **Application Architecture:**
  - **Razor Pages 模式：** Page Model (MVVM-like) 處理頁面邏輯
  - **簡單的單體應用**（MVP 階段）- 前後端整合在同一專案
  - **分層設計：**
    - Presentation Layer: Razor Pages + ViewModels
    - Business Layer: Services (PostService, AuthService)
    - Data Layer: EF Core Repositories + DbContext
  - **優點：**
    - 開發與部署簡單
    - 單一容器即可運行
    - SEO 友善（伺服器端渲染）
    - 容易測試與維護

- **Integration Requirements:**
  - Google OAuth API
  - 可能的第三方整合：圖片 CDN、留言系統、Analytics

- **Security/Compliance:**
  - HTTPS 強制執行
  - CSRF/XSS 防護
  - 環境變數管理（不將敏感資訊寫入程式碼或映像檔）
  - 容器安全掃描（如 Trivy）
  - GDPR 考量（如整合 Analytics 時）

---

## Constraints & Assumptions

### Constraints

- **Budget:** 個人專案，盡量使用免費或低成本服務（Azure Free Tier / 開源工具）
- **Timeline:** 目標 3-6 個月完成 MVP（取決於可投入時間）
- **Resources:** 單人開發（或小型團隊協作）
- **Technical:**
  - 需依賴穩定的 .NET 生態系與 Google OAuth 服務
  - 初期不考慮高併發情境（個人部落格流量有限）

### Key Assumptions

- 管理員具備基本的 .NET 部署知識（能設定環境變數、部署應用）
- **開發環境具備 Podman（或 Docker）容器執行能力**
- **Podman 與 Docker 的相容性足以滿足部署需求**（無需使用 Docker 專屬功能）
- Google OAuth 服務持續穩定可用
- 白名單管理員模式足以滿足個人部落格需求（無需複雜權限系統）
- 讀者主要透過桌面或手機瀏覽器存取，不需要原生應用
- Markdown 或 WYSIWYG 編輯器足以滿足內容創作需求
- 初期流量較低，標準的關聯式資料庫即可處理
- 容器化部署可在各種雲端或自架環境執行

---

## Risks & Open Questions

### Key Risks

- **設計實作挑戰：** UI/UX 設計是核心價值，若設計品質不佳，產品失去差異化優勢
  - **緩解措施：** 已完成設計系統規格定義，使用 Tailwind CSS 確保一致性

- **功能範圍蔓延：** 容易想要加入更多功能而延遲 MVP
  - **緩解措施：** 嚴格遵守 MVP 範圍定義，Phase 2 功能明確延後

- **Google OAuth 依賴：** 若 Google 服務中斷或政策變更
  - **緩解措施：** 保留未來支援其他 OAuth Provider 的架構彈性

- **Tailwind CSS 整合：** 在 Razor Pages 中設定 Tailwind CSS 建置流程
  - **緩解措施：** 使用成熟的整合方案（PostCSS + Tailwind CLI）

### Open Questions

_**✅ 核心決策 100% 完成！** 以下僅為 MVP 後考慮的次要問題。_

#### 次要決策（開發過程中決定，不影響架構）：

- **PostgreSQL 部署方式（生產環境）：**
  - 開發環境：容器內 PostgreSQL（docker-compose）✅ 已確定
  - 生產環境：容器內 PostgreSQL vs 託管服務（Azure Database, AWS RDS）
  - **決定時機：** 準備正式上線時（1-2 週前）

#### MVP 後考慮的問題：

- **分析工具整合：**
  - 選項：Google Analytics, Plausible, Umami, 或不使用
  - 考量：隱私（GDPR）、效能影響
  - **決定時機：** MVP 上線後，根據實際需求

- **備份策略：**
  - PostgreSQL 自動備份頻率與保留時間
  - 備份儲存位置（本地 Volume, 雲端儲存）
  - **決定時機：** 正式上線前

- **部署平台：**
  - Fly.io, Railway, DigitalOcean VPS, Azure Container Apps, 或其他
  - **決定時機：** MVP 開發完成，準備上線前 1-2 週
  - **優勢：** 容器化讓平台遷移容易

- **CI/CD 流程：**
  - GitHub Actions, GitLab CI, 或手動部署
  - 自動測試範圍
  - **決定時機：** MVP 後，需要頻繁更新時

### Areas Needing Further Research

_**核心決策已完成 ✅** 以下為實作細節研究，開發時進行即可。_

#### 已完成的決策：

- **✅ UI/UX 設計系統：** 已完成定義（Azure Blue + 白色系統，思源黑體，現代專業風格）
- **✅ 前端架構：** 已確定使用 Razor Pages（SEO 友善、成熟穩定）
- **✅ 資料庫：** 已確定使用 PostgreSQL（功能完整、容器化簡單、開發/生產一致）
- **✅ 內容格式：** 已確定使用 Markdown（Markdig 後端處理 + EasyMDE/SimpleMDE 前端編輯）
- **✅ 文章 URL：** 中文 Slug 支援（自動生成，可手動修改）
- **✅ 圖片策略：** MVP 只支援外部連結，Phase 2 實作上傳
- **✅ SEO 策略：** MVP 包含基本 Meta Tags + Open Graph + Twitter Card
- **✅ 分頁方式：** 傳統分頁（每頁 10 篇）
- **✅ 時區設定：** 台灣時間 UTC+8，格式「年月日時分秒」
- **✅ Web 伺服器：** 不需要 NGINX，ASP.NET Core Kestrel 直接服務

#### 實作細節研究（開發時進行）：

1. **Tailwind CSS 整合：**
   - 建置流程設定（PostCSS, Tailwind CLI）
   - 套用 Design Tokens（tailwind.config.js 配置）
   - 生產環境 CSS 優化

2. **Markdown 處理：**
   - Markdig 擴充套件配置（GFM、程式碼高亮、表格）
   - XSS 防護（Markdown 輸入清理）
   - 中文 Slug 生成邏輯

3. **編輯器整合：**
   - EasyMDE vs SimpleMDE 最終選擇
   - 自訂樣式以符合設計系統
   - 與 Razor Pages 表單整合

4. **程式碼高亮：**
   - highlight.js vs Prism.js 選擇
   - 主題配色（配合設計系統）
   - JetBrains Mono 字型整合

5. **PostgreSQL + EF Core：**
   - Entity Models 設計（Post, Admin）
   - Migration 策略
   - 連線字串管理（環境變數）

6. **SEO 實作：**
   - Razor Pages 中 Meta tags 管理模式
   - Open Graph / Twitter Card 實作
   - Canonical URLs 處理

7. **Google OAuth 整合：**
   - Authentication Middleware 設定
   - 環境變數白名單機制
   - Cookie/Session 管理

8. **容器化開發環境：**
   - Dockerfile 多階段建置
   - docker-compose.yml 配置
   - Hot Reload 在容器內設定
   - Volume 權限處理

9. **字型載入優化：**
   - Google Fonts (Noto Sans TC) 載入
   - JetBrains Mono 本地或 CDN
   - font-display 策略與 preload

10. **分頁實作：**
    - Razor Pages 分頁邏輯
    - 分頁導航 UI 元件
    - SEO 友善的分頁 URL

---

## Appendices

### A. Research Summary

_（目前尚無正式研究，以下為初步觀察）_

**市場觀察：**
- Ghost CMS 在技術部落客中頗受歡迎，其設計簡潔、效能佳
- Medium 的閱讀體驗優秀，但平台限制多
- 許多開發者傾向自架部落格以完整控制

**技術可行性：**
- .NET Core 的效能與開發體驗已大幅改善
- Google OAuth 整合相對成熟，有豐富的文件與範例

### B. Stakeholder Input

_（個人專案，主要由專案擁有者決策）_

### C. References

**技術文件：**
- ASP.NET Core 文件：https://docs.microsoft.com/aspnet/core
- **Razor Pages 官方文件：** https://docs.microsoft.com/aspnet/core/razor-pages/
- **Razor Pages 教學：** https://docs.microsoft.com/aspnet/core/tutorials/razor-pages/
- Entity Framework Core：https://docs.microsoft.com/ef/core/
- **EF Core with PostgreSQL：** https://www.npgsql.org/efcore/
- .NET Docker 官方映像：https://hub.docker.com/_/microsoft-dotnet-aspnet
- PostgreSQL Docker 映像：https://hub.docker.com/_/postgres
- Podman 文件：https://docs.podman.io/
- Docker Compose 規格：https://docs.docker.com/compose/
- Google OAuth 2.0 文件：https://developers.google.com/identity/protocols/oauth2

**Markdown & 編輯器：**
- **Markdig（C# Markdown 處理器）：** https://github.com/xoofx/markdig
- **EasyMDE（Markdown 編輯器）：** https://github.com/Ionaru/easy-markdown-editor
- **SimpleMDE（Markdown 編輯器）：** https://github.com/sparksuite/simplemde-markdown-editor
- Highlight.js（程式碼高亮）：https://highlightjs.org/
- Prism.js（程式碼高亮）：https://prismjs.com/
- CommonMark 規範：https://commonmark.org/
- GitHub Flavored Markdown (GFM)：https://github.github.com/gfm/

**設計系統 & UI 框架：**
- Tailwind CSS：https://tailwindcss.com
- **Tailwind CSS with .NET：** https://tailwindcss.com/docs/installation/play-cdn
- Alpine.js（輕量前端互動）：https://alpinejs.dev/
- Google Fonts (Noto Sans TC)：https://fonts.google.com/noto/specimen/Noto+Sans+TC
- JetBrains Mono：https://www.jetbrains.com/lp/mono/
- Fira Code：https://github.com/tonsky/FiraCode
- CSS Variables 規範：https://developer.mozilla.org/en-US/docs/Web/CSS/Using_CSS_custom_properties

**設計參考：**
- Ghost CMS（現代專業部落格設計）：https://ghost.org
- Medium（閱讀體驗參考）：https://medium.com
- Microsoft Fluent Design（Azure Blue 設計系統）：https://www.microsoft.com/design/fluent/

**效能 & SEO：**
- Lighthouse 效能指標：https://web.dev/performance-scoring/
- Web Vitals：https://web.dev/vitals/
- WCAG 無障礙標準：https://www.w3.org/WAI/WCAG21/quickref/

---

## Next Steps

### Immediate Actions

_**所有核心決策已完成 ✅** 以下為開發步驟，可按順序執行。_

#### ✅ 已完成的決策：
1. **✅ 設計系統規格**：Azure Blue + 白色、思源黑體、現代專業風格、完整 Design Tokens
2. **✅ 前端架構**：Razor Pages（SEO 友善、成熟穩定）
3. **✅ 資料庫**：PostgreSQL（開發/生產一致）
4. **✅ 內容格式**：Markdown（Markdig + EasyMDE/SimpleMDE）
5. **✅ 文章 URL**：中文 Slug（自動生成，可手動修改）
6. **✅ 圖片策略**：MVP 只支援外部連結
7. **✅ SEO 策略**：包含基本 Meta Tags + Open Graph + Twitter Card
8. **✅ 分頁方式**：傳統分頁（每頁 10 篇）
9. **✅ 時區設定**：台灣時間 UTC+8
10. **✅ Web 伺服器**：不需要 NGINX

#### 🚀 開發步驟：

**步驟 1：專案初始化**
- 使用 `dotnet new razor -n dotnet-blog-bmad` 建立專案
- 安裝 NuGet 套件：
  - `Npgsql.EntityFrameworkCore.PostgreSQL`
  - `Markdig`
  - `Microsoft.AspNetCore.Authentication.Google`
- 初始化 Git repository
- 建立資料夾結構：`Pages/`, `Services/`, `Data/`, `wwwroot/`

**步驟 2：容器化開發環境**
- 建立 `Dockerfile`（.NET 8 多階段建置，Debian 基底）
- 建立 `docker-compose.yml`（Podman 相容）：
  - 服務 1: `app` - ASP.NET Core（port 5000/5001）
  - 服務 2: `db` - PostgreSQL 15（port 5432）
  - Volume: `postgres-data`（資料持久化）
- 建立 `.env.example` 範本（資料庫連線、OAuth、白名單 Email）
- 驗證 `podman-compose up` 運行正常
- 測試 Hot Reload 在容器內運作

**步驟 3：資料模型與 EF Core**
- 定義 Entity Models：
  - `Post`：Id, Title, Slug, Content, IsPublished, PublishedAt, CreatedAt, UpdatedAt
  - `Admin`：Id, Email, Name, CreatedAt
- 建立 `ApplicationDbContext`
- 設定連線字串（從環境變數 `ConnectionStrings__DefaultConnection`）
- 安裝 EF Tools：`dotnet tool install --global dotnet-ef`
- 執行 Migration：`dotnet ef migrations add InitialCreate`
- 驗證資料庫連線

**步驟 4：Tailwind CSS 整合**
- 安裝 Node.js 套件：`npm install -D tailwindcss postcss autoprefixer`
- 初始化 Tailwind：`npx tailwindcss init`
- 配置 `tailwind.config.js`（套用 Design Tokens：顏色、字型、間距等）
- 建立 `wwwroot/css/site.css`（@tailwind base/components/utilities）
- 設定建置流程（package.json scripts）
- 測試樣式渲染

**步驟 5：基礎 Razor Pages 架構**
- 建立 Layout：`Pages/Shared/_Layout.cshtml`
  - 套用 Tailwind CSS
  - 載入字型（Noto Sans TC + JetBrains Mono）
  - 實作設計系統（導覽列、Footer）
- 建立前台頁面：
  - `Pages/Index.cshtml` - 首頁
  - `Pages/Posts/Index.cshtml` - 文章列表（含分頁）
  - `Pages/Posts/Post.cshtml` - 文章內容頁（`/posts/{slug}`）
- 實作分頁邏輯（每頁 10 篇）

**步驟 6：Markdown 處理**
- 整合 Markdig（Service 層）
- 配置 Markdig Pipeline（GFM, 表格，程式碼高亮）
- 實作中文 Slug 生成邏輯
- 實作 Markdown → HTML 轉換
- 整合程式碼高亮（highlight.js 或 Prism.js）
- XSS 防護處理

**步驟 7：後台 Markdown 編輯器**
- 選擇 EasyMDE 或 SimpleMDE
- 建立後台頁面：
  - `Pages/Admin/Posts/Index.cshtml` - 文章管理列表
  - `Pages/Admin/Posts/Create.cshtml` - 新增文章
  - `Pages/Admin/Posts/Edit.cshtml` - 編輯文章
- 整合 Markdown 編輯器
- 實作 CRUD 功能
- 測試 Slug 自動生成與手動修改

**步驟 8：Google OAuth 認證**
- 在 Google Cloud Console 建立 OAuth 2.0 憑證
- 設定 Authentication Middleware（`Program.cs`）
- 實作環境變數白名單檢查（`ALLOWED_ADMIN_EMAIL`）
- 建立登入/登出頁面
- 保護後台路由（Authorization Policy）
- 測試認證流程

**步驟 9：SEO 實作**
- 建立 SEO Helper / Base Page Model
- 實作 Meta Tags 管理（title, description）
- 實作 Open Graph Tags
- 實作 Twitter Card Tags
- 實作 Canonical URL
- 每個頁面設定 SEO 資訊
- 用 Facebook Debugger / Twitter Card Validator 驗證

**步驟 10：時區與最終測試**
- 實作時區轉換邏輯（UTC → UTC+8）
- 設定時間顯示格式（`YYYY-MM-DD HH:mm:ss`）
- 完整功能測試：
  - 認證流程
  - 文章 CRUD
  - 中文 Slug URL
  - 分頁導航
  - 響應式設計（桌面/平板/手機）
  - Markdown 渲染
  - 程式碼高亮
- Lighthouse 測試（Performance, SEO, Accessibility）

### PM Handoff

本專案簡報提供了 **.NET 個人部落格系統**的完整背景資訊。下一階段請進入「PRD 生成模式」，根據本簡報與使用者協作建立詳細的產品需求文件（PRD），逐一定義功能規格、使用者故事、技術細節與驗收標準。
