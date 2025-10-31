# dotnet-blog-bmad

個人部落格系統 - 使用 ASP.NET Core 8.0 Razor Pages 建構

## 專案簡介

這是一個**試驗性專案**，主要目的是驗證和展示 **BMAD-METHOD** 套件在實際開發中的應用效果。本專案開發一個功能完整的個人部落格系統，採用 .NET 8.0 Razor Pages 框架，使用 PostgreSQL 作為資料庫。

### 什麼是 BMAD-METHOD？

**BMAD (Business Model-Aligned Development)** 是一個系統化的軟體開發方法論，透過多個專業代理（Agent）協作，從規劃到開發實現完整的工作流程。本專案完整遵循 BMAD 流程，展示該方法論如何協助開發者有效率地完成軟體專案。

### 專案目標

1. **驗證 BMAD-METHOD**：透過實際專案展示 BMAD 方法論的實用性與效果
2. **學習與實驗**：作為學習 ASP.NET Core 與現代開發實踐的實驗場域
3. **可用的部落格系統**：產出一個真正可用的個人部落格平台

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

專案目前已完成主要核心功能，正處於進階功能開發階段：

### 已完成功能

- [x] **Story 1.x - 基礎建設**
  - [x] Story 1.1: 專案初始化與資料夾結構
  - [x] Story 1.2: Container 開發環境設定
  - [x] Story 1.3: Entity Framework Core 設定
  - [x] Story 1.4: Google OAuth 身份驗證
  - [x] Story 1.5: 基礎頁面與導覽

- [x] **Story 2.x - 核心功能**
  - [x] Story 2.1: 文章 CRUD 功能
  - [x] Story 2.2: Markdown 編輯器與預覽
  - [x] Story 2.3: 文章列表與分頁

- [x] **Story 3.x-4.x - 進階功能**
  - [x] Story 3.1-4.1: 進階功能與效能優化

### 最近更新

- 修復 Google OAuth 登出流程
- 修復 Entity Framework 時間戳記處理邏輯
- 優化 Docker 網路設定（使用 host network mode）
- 完善表單驗證腳本引用

完整的功能規劃請參閱 [PRD 文件](docs/prd.md)。

## 文件

### 專案文件

- [專案簡介 (Project Brief)](docs/brief.md)
- [產品需求文件 (PRD)](docs/prd.md)
- [架構設計文件 (Architecture)](docs/architecture.md)

### BMAD-METHOD 相關

- [BMAD-METHOD 工作流程圖](bmad-method-workflow.md) - 完整的開發流程說明與 Mermaid 流程圖

## BMAD-METHOD 開發流程

本專案完整採用 **BMAD-METHOD** 套件進行開發，展示其工作流程：

### 📋 規劃階段（Planning Phase）

規劃階段由三個專業代理協作，產出完整的專案規劃文件：

- **👤 Analyst（分析師）**：收集需求、定義問題，產出專案簡介
- **📱 PM（產品經理）**：整理成 PRD（產品需求文件），定義功能規格
- **🏗️ Architect（架構師）**：設計技術架構、資料庫結構與系統設計

完成後需要人工審查，確認無誤後產出完整的規劃文件。

### ⚙️ 開發階段（Development Phase）

規劃完成後進入實作流程：

- **📝 Scrum Master（敏捷大師）**：將規劃文件轉換成詳細的 Story 檔案
- **💻 Dev（開發者）**：讀取 Story 並進行程式碼實作
- **🧪 QA（測試）**：驗證程式碼品質與功能正確性

測試通過後繼續下一個 Story，直到所有 Story 完成。

### 流程圖

完整的 BMAD-METHOD 工作流程請參考：[bmad-method-workflow.md](bmad-method-workflow.md)

### 核心特色

- **多代理協作**：規劃階段透過多個代理協作產出前後一致的完整規劃
- **Story 檔案系統**：開發階段的 Story 檔案讓每個任務都有完整情境
- **持續驗證**：每個 Story 完成後都經過測試，確保品質

## 授權

本專案僅供個人學習與使用。

## 聯絡資訊

如有任何問題或建議，請透過 Issues 回報。
