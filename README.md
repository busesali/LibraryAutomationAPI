# Library Automation API

## Genel Bakış

LibraryAutomationAPI, kütüphane kaynaklarını dijital olarak yönetmek amacıyla geliştirilmiş bir ASP.NET Core Web API uygulamasıdır. Kullanıcılar sisteme kayıt olabilir, giriş yapabilir ve JWT tabanlı kimlik doğrulama ile kitap ve kategori işlemlerini güvenli şekilde gerçekleştirebilir. SQL Server veritabanı ile çalışan bu sistem, Swagger UI sayesinde geliştiricilere kolay test imkanı sunar.

## Özellikler

- Kullanıcı kayıt ve giriş işlemleri (JWT ile kimlik doğrulama)
- Kitap ekleme, listeleme, güncelleme ve silme işlemleri
- Kategori ekleme ve listeleme
- Swagger UI üzerinden API test desteği
- Yetkilendirme kontrollü uç noktalar
- SQL Server veritabanı desteği
- Entity Framework Core ile veri yönetimi

## Kullanılan Teknolojiler

- ASP.NET Core 6 Web API
- Entity Framework Core
- SQL Server
- JWT (JSON Web Token) Authentication
- Swagger (Swashbuckle)
- LINQ, Auto Mapping, Middleware yapısı

## Gereksinimler

Bu projeyi çalıştırmak için aşağıdaki yazılımların sisteminizde kurulu olması gerekir:

- .NET 6 SDK  
- SQL Server (Express veya tam sürüm)

## Önemli API Endpointleri

- `POST /api/auth/register` → Yeni kullanıcı kaydı  
- `POST /api/auth/login` → Giriş yap ve token al  
- `GET /api/books` → Kitapları listele  
- `POST /api/books` → Yeni kitap ekle  
- `PUT /api/books/{id}` → Kitap bilgilerini güncelle  
- `DELETE /api/books/{id}` → Kitap sil  
- `GET /api/categories` → Kategorileri listele  
- `POST /api/categories` → Yeni kategori ekle

## Lisans

Bu proje MIT Lisansı ile lisanslanmıştır. İsteyen herkes kopyalayabilir, dağıtabilir ve değiştirebilir.
