# BMAD-METHOD 工作流程

## 流程圖

```mermaid
graph TB
    Start([開始新專案]) --> Install[安裝 BMAD-METHOD]
    
    Install --> Planning[📋 規劃階段<br/>Planning Phase]
    
    subgraph Planning_Phase [" "]
        Planning --> Analyst[👤 Analyst<br/>分析師<br/>收集需求、定義問題]
        Analyst --> PM[📱 PM<br/>產品經理<br/>產出 PRD 文件]
        PM --> Architect[🏗️ Architect<br/>架構師<br/>設計技術架構]
        Architect --> Review{人工審查<br/>規劃文件}
        Review -->|需要修改| Analyst
        Review -->|確認完成| PlanDone[✅ 規劃完成<br/>PRD + 架構文件]
    end
    
    PlanDone --> Development[⚙️ 開發階段<br/>Development Phase]
    
    subgraph Dev_Phase [" "]
        Development --> SM[📝 Scrum Master<br/>敏捷大師<br/>將文件轉換成詳細 Story]
        SM --> Stories[(Story Files<br/>開發故事檔案<br/>包含完整情境)]
        Stories --> Dev[💻 Dev<br/>開發者<br/>讀取 Story 並實作]
        Dev --> Code[程式碼實作]
        Code --> QA[🧪 QA<br/>測試<br/>驗證程式碼品質]
        QA --> Test{測試結果}
        Test -->|問題| Dev
        Test -->|通過| NextStory{還有其他 Story?}
        NextStory -->|是| Stories
        NextStory -->|否| Complete
    end
    
    Complete([🎉 專案完成])
    
    style Planning fill:#e1f5ff
    style Development fill:#fff4e1
    style Start fill:#90EE90
    style Complete fill:#90EE90
    style Review fill:#FFE4B5
    style Test fill:#FFE4B5
    style Stories fill:#F0E68C
```

## 流程說明

### 📋 規劃階段（Planning Phase）

這個階段由三個專業代理協作，產出完整的專案規劃文件：

- **👤 Analyst（分析師）**：收集需求、定義問題
- **📱 PM（產品經理）**：整理成 PRD（產品需求文件）
- **🏗️ Architect（架構師）**：設計技術架構

完成後需要人工審查，確認無誤後產出 PRD + 架構文件。

### ⚙️ 開發階段（Development Phase）

規劃完成後進入開發流程：

- **📝 Scrum Master（敏捷大師）**：將規劃文件轉換成詳細的 Story 檔案
- **💻 Dev（開發者）**：讀取 Story 並進行程式碼實作
- **🧪 QA（測試）**：驗證程式碼品質

測試通過後繼續下一個 Story，直到所有 Story 完成。

### 核心特色

**規劃階段**透過多個代理協作產出前後一致的完整規劃。

**開發階段**的 Story 檔案系統讓每個開發任務都有完整情境，開發者不會迷失方向。
