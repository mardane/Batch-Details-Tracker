# Instacart Batch Tracker – Full-Stack Development Roadmap

## 📋 Project Overview

**Purpose:** A cross-platform app for tracking Instacart batch details: progress, profit, time spent, miles driven, and daily momentum.

**Tech Stack:**
- **Back-end:** ASP.NET Core 10 (Blazor Server)
- **Web UI:** Blazor Server + Bootstrap (responsive)
- **Mobile:** Blazor Hybrid (MAUI) + Web wrapper
- **Data:** JSON-based local storage (MVP) → SQLite/SQL Server (Phase 2+)

---

## 🗺️ Phases

### **Phase 1: Web MVP** ✅ COMPLETE
**Duration:** 1–2 weeks | **Goal:** Launch functional web dashboard

**Deliverables:**
- ✅ Dashboard home with total profit, weekly earnings, active batch count, average per batch  
- ✅ **Batches** page: Quick entry form + recent activity list  
  - Fields: Batch name, store, date, status (Active/Completed/Cancelled), orders, base pay, tips, adjustments, miles, hours spent, notes  
  - Calculated metrics: Total earnings, profit/hour, profit/order  
- ✅ **Reports** page: Lightweight snapshot view  
  - Total profit, weekly earnings, average per batch, best batch  
  - Status breakdown (charts), top opportunities  
- ✅ Responsive layout (mobile-first, tested on Windows desktop)  
- ✅ Local JSON storage (seeded with sample data)  
- ✅ Clean UI with stat cards, status pills, progress bars  

**Deployment:** localhost:5034 (run locally with `dotnet run`)

---

### **Phase 2: Mobile - Android (Blazor Hybrid)**
**Duration:** 2–3 weeks | **Goal:** Native Android app with offline sync

**Deliverables:**
- Blazor Hybrid UI (MAUI) wrapping web components  
- Native navigation + gesture support  
- Offline-first SQLite database  
- Background sync when online  
- Push notifications for completed batches  
- Publish to Google Play Store

**Technical Notes:**
- Reuse Blazor components from Phase 1  
- Add platform-specific handlers (camera for receipts, geolocation)

---

### **Phase 3: Mobile - iOS (Blazor Hybrid)**
**Duration:** 2–3 weeks | **Goal:** Native iOS app with feature parity to Android

**Deliverables:**
- Blazor Hybrid UI (MAUI) for iOS  
- Native navigation + SwiftUI integration  
- Offline SQLite, background sync  
- Push notifications  
- Publish to Apple App Store

**Technical Notes:**
- Parallel development with Android using shared MAUI codebase  
- Platform-specific handlers (HealthKit for step count, geofencing)

---

### **Phase 4: Backend API & Database** (Optional – for multi-device sync)
**Duration:** 3–4 weeks | **Goal:** Centralized data storage with cloud sync

**Deliverables:**
- RESTful API (ASP.NET Core Web API)  
- SQL Server database (batches, user accounts, analytics)  
- JWT authentication  
- Sync layer: Device ↔ Cloud  
- Cloud deployment (Azure App Service + SQL Database)

**Technical Notes:**
- Maintain offline-first approach on mobile  
- Conflict resolution for edits across devices

---

### **Phase 5: Analytics & Insights** (Optional – Post-MVP)
**Duration:** 2–3 weeks | **Goal:** Advanced reporting and trend analysis

**Deliverables:**
- Graphs: Daily/weekly earnings trends  
- Heatmaps: Best time-of-day, best locations  
- Batch performance scoring  
- Break-even analysis per batch  
- Export reports (PDF, CSV)  
- Admin dashboard for metrics

---

## 🎯 MVP Feature Checklist (Phase 1)

- [x] Responsive web UI (desktop + mobile browser)  
- [x] Dashboard KPIs (total profit, weekly earnings, active count, avg/batch)  
- [x] Add new batches with form validation  
- [x] View recent batches with quick stats  
- [x] Basic reports (status breakdown, top batches)  
- [x] Local JSON persistence  
- [x] Seeded sample data  
- [x] Clean design with Indigo/Cyan color scheme  

---

## 📱 Mobile Strategy

### **Blazor Hybrid (MAUI)**
- Single codebase for Android + iOS  
- Reuse ~70% of Blazor components  
- Native performance with web flexibility  
- Faster time-to-market than separate native apps  

### **Distribution:**
1. **Phase 2 (Android):** Google Play Store  
2. **Phase 3 (iOS):** Apple App Store  
3. **Web:** ASP.NET Core on Azure (accessible via browser)

---

## 🔄 Data Flow (MVP)

```
User Input (Form)
    ↓
Blazor Component
    ↓
BatchTrackerService (in-memory)
    ↓
JSON File (App_Data/batches.json)
    ↓
Load on app start (seeded if empty)
    ↓
Display in Dashboard/Batches/Reports
```

**Future (Phase 4):**
```
Device JSON ↔ SQLite (offline) ↔ API ↔ SQL Server
```

---

## 🚀 Running the MVP

### **Start the web app:**
```powershell
cd c:\Sanaz-Projects\Batch-Details-Tracker
dotnet run --project src/BatchDetailsTracker.Web --urls "http://localhost:5034"
```

### **Access in browser:**
- **Desktop:** http://localhost:5034  
- **Mobile (same network):** http://<your-pc-ip>:5034  

### **Build for production:**
```powershell
dotnet publish -c Release -o ./publish
```

---

## 📊 Project Structure

```
Batch-Details-Tracker/
├── src/
│   └── BatchDetailsTracker.Web/
│       ├── Components/
│       │   ├── Pages/
│       │   │   ├── Home.razor          (Dashboard)
│       │   │   ├── Batches.razor       (Entry + List)
│       │   │   └── Reports.razor       (Analytics)
│       │   └── Layout/
│       │       ├── NavMenu.razor
│       │       └── MainLayout.razor
│       ├── Models/
│       │   └── BatchRecord.cs          (Data model)
│       ├── Services/
│       │   └── BatchTrackerService.cs  (Business logic)
│       ├── wwwroot/
│       │   └── app.css                 (Responsive styling)
│       ├── Program.cs
│       └── App.razor
├── BatchDetailsTracker.sln
└── .git/
```

---

## 🎨 Design System (MVP)

| Element | Color | Usage |
|---------|-------|-------|
| Primary | Indigo (#4338ca) | Buttons, links, highlights |
| Secondary | Cyan (#0ea5e9) | Accents, gradients |
| Success | Green (#16a34a) | Completed status |
| Active | Blue (#2563eb) | In-progress status |
| Neutral | Gray (#6b7280) | Text, borders |
| Background | Light (#f7f8fc) | Page background |

---

## 📈 Success Metrics

- **Phase 1:** Web app loads in <2s, form submits successfully, recent data displays  
- **Phase 2:** Android app installs, runs offline, syncs on reconnect  
- **Phase 3:** iOS app feature-parity with Android  
- **Phase 4:** Multi-device sync without conflicts  
- **Phase 5:** Users gain actionable insights from analytics  

---

## 🔄 Next Steps

1. **Phase 1 (Now):** Test MVP locally, gather feedback  
2. **Phase 2:** Set up MAUI project, begin Android UI  
3. **Phase 3:** Port MAUI codebase to iOS, test on simulator  
4. **Phase 4 (Optional):** Design API schema, set up backend  

---

**Status:** Phase 1 ✅ Complete | Ready for Phase 2 (Android/MAUI)
