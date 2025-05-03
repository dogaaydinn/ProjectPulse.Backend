# ✅ ProjectPulse.Backend - TODO & Architecture Notes

## 📌 Architectural Decisions

* **Clean Architecture** uygulanıyor: Domain, Application, Infrastructure, Web API katmanları ayrıldı.
* **CQRS (Command Query Responsibility Segregation)** yapısı kullanılıyor.
* **Manual validation** kullanılıyor (FluentValidation yerine `IValidator<T>` + `ValidationResult`).
* **Custom Error ve Result pattern** uygulanıyor: `Shared.Results.Error` ve `Result<T>`.
* **Factory Pattern**: `ProjectFactory`, `TaskFactory`, `WorkflowFactory`, `CommentFactory` gibi Domain-Driven yaklaşım ile entity creation kontrollü hale getirildi.
* **AutoMapper** sadece DTO <-> Entity dönüşümünde, sınırlı ve kontrollü kullanılıyor.

---

## 🔨 Uygulanan Tasarım Desenleri (Design Patterns)

* **Factory Pattern**: Task, Project, Workflow ve Comment oluştururken kullanılıyor.
* **Repository Pattern**: `IProjectRepository`, `ITaskRepository` vb. ile `DbContext` erişimi soyutlandı.
* **Unit of Work**: Transaction yönetimi için kullanılıyor (`IUnitOfWork`).
* **Custom Exception Handling**: `AppException`, `ValidationException` gibi sınıflarla merkezi hata yönetimi yapılıyor.

---

## ✅ DTO & Mapping Stratejisi

* **AutoMapper** sadece `Project` için kullanıldı.

    * `Project`, `CreateProjectRequest`, `UpdateProjectRequest` <-> `ProjectDto` dönüşümleri `ProjectMappingProfile` içinde tanımlı.
* Diğer entity'ler için `Manual Mapping` tercih ediliyor (performans ve kontrol açısından).

> 🎯 AutoMapper sadece complex mapping'lerde ve büyük nesne dönüşümlerinde kullanılacak. Basit DTO dönüşümlerinde manuel yazılacak.

---

## 🧪 Validation Sistemi

* `Application.Common.Validation` içinde `IValidator<T>` ve `ValidationResult` var.
* Her Command ve Query için manuel validator yazılıyor.
* Hatalar `ValidationMessages` constant yapısından okunuyor.

---

## 🔐 Security & Authentication

* Planlanan:

    * JWT tabanlı authentication
    * Role-based authorization (`GlobalRole`, `TaskRole`, `ProjectRole`)

---

## 📦 Infrastructure Planları

* Logging: Serilog / centralized logging
* Exception handling middleware
* Dockerize edilecek
* CI/CD: GitHub Actions ile build/test pipeline
* Swagger dokümantasyonu
* Email notification (domain event sonrası)

---

## 🚧 TODO Checklist (günlük olarak güncellenecek)

* [x] Project CRUD
* [x] Task CRUD
* [x] Workflow + Status CRUD
* [x] Comment CRUD
* [ ] Notification alt yapısı
* [ ] Team & UserTeam yönetimi
* [ ] TaskAssignment, TaskLabel, TaskDependency ilişkileri
* [ ] Custom ValidationException ile centralized validation error handling
* [ ] Authorization middleware
* [ ] Project/Task/Comment DTO'lar AutoMapper dışı yapılarda da tamamlanacak
* [ ] Dockerfile + docker-compose
* [ ] Unit Test + Integration Test yapısı kurulacak

---

## 🧠 Notlar

* Her servis `Service` klasöründe ve Interface'i `Application.Interfaces` içinde.
* Command & Query handler'lar sadece `IRepository`, `IUnitOfWork`, `IFactory` ile işlem yapar.
* Controller -> Service -> Command/Query yapısı izleniyor (Controller Business Logic içermez).

> ⚠️ Tüm command ve query'lerde validation yapılmadan işlem yapılmaz.

---

Son güncelleme: \[Otomatik güncellenmeli veya manuel eklenmeli]

## 📅 Günlük Geliştirme Notları(3 mayıs 21.03)
📌 Uygulanacak Yeni Özellikler ve Adımlar (TODO LİSTESİ)

## 📌 Uygulanacak Yeni Özellikler ve Adımlar (TODO LİSTESİ)

### 1️⃣ Project Feature
- **DTO'lar**: `ProjectDto`, `CreateProjectRequest`, `UpdateProjectRequest`
- **Mapping**: AutoMapper
- **Command & Queries**:
  - 🟢 Create
  - 🟢 GetById
  - 🟢 GetAll
  - 🟢 Update
  - 🟢 Delete
- **Service Katmanı**: `ProjectService`
- **Validation**: ✅
- **Result + Error Handling**: ✅
- **Unit Tests**: ⬜ (ileride)
- **Authorization Rules**: ⬜ (ileride)
- **Project Assignment via Role**: Admin/Manager ✅

---

### 2️⃣ Task Feature
- **Entity**: `TaskItem` yapısı ✅
- **Factory**: `TaskFactory`, `ITaskFactory` ✅
- **DTO'lar**: `TaskDto`, `CreateTaskRequest`, `UpdateTaskRequest` ✅
- **Command/Query/Validation/Service** yapısı ✅
- **İlişkiler**:
  - 🟢 Status & Schedule
  - ⬜ Task Assignment & Dependency yönetimi

---

### 3️⃣ Workflow & Status
- **Repository**: `WorkflowRepository` ✅
- **Queries**:
  - 🟢 `GetWorkflowsByProjectIdQuery` handler tamamlandı
- **CRUD İşlemleri**:
  - 🟢 Workflow create/update/delete
  - 🟢 Status create/update/delete
- **Status İşlemleri**:
  - ⬜ Status reordering & transitions (zorunlu olacak)
- **Validation**: ✅ Workflow validation

---

### 4️⃣ Comment Feature
- **Entity & Factory**: `Comment`, `CommentFactory` ✅
- **CRUD İşlemleri**:
  - 🟢 Basic Comment Create Handler
  - 🟢 List Comments by Task
  - 🟢 Soft delete / update comment
- **Validation**: 🟢 Author validation (user matching)

---

### 5️⃣ Validation Sistemimiz
- **Yapı**:
  - 🟢 `ValidationResult`, `IValidator<T>`
  - 🟢 Central error list: `ValidationMessages`, `ErrorMessages`, `ErrorCodes`
- **Exception**:
  - 🟢 Tek tip `ValidationException` yapısı
  - 🟢 Otomatik controller-level validation feedback (middleware ya da handler bazlı)

---

### 6️⃣ Mapping Yapısı
- **AutoMapper**:
  - 🟢 `ProjectMappingProfile`
  - 🟢 `TaskMappingProfile`, `CommentMappingProfile` gibi diğerleri
- **Alternatif**:
  - ⬜ Mapping yerine `MapToDto()` gibi extension method alternatifi (Opsiyonel)

---

### 7️⃣ Documentation
- **Dosyalar**:
  - 🟢 `docs/ARCHITECTURE_OVERVIEW.md`
  - 🟢 `docs/DECISIONS_LOG.md`
  - 🟢 `docs/SECURITY_NOTES.md`
  - 🟢 `docs/DEPLOYMENT_GUIDE.md`
  - 🟢 `docs/README.md` (ana giriş)
- **Diyagram**:
  - ⬜ Entity Relations diyagramı (draw.io / .png)

---

### 8️⃣ Advanced Backend Özellikleri (Sonraki Fazlarda)
- **Authentication**: ⬜ JWT veya Identity + refresh token
- **Authorization**: ⬜ Role-based + policy
- **Logging**: ⬜ Serilog veya CleanLogger abstraction
- **Exception Handling Middleware**: ⬜
- **Dockerize**: ⬜ Environment setup
- **DevOps**:
  - ⬜ CI/CD pipeline (GitHub Actions veya Azure DevOps)
- **Database**:
  - ⬜ Seeding (fake data + migrations)
```