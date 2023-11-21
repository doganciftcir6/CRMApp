# Onicorn.CRMApp.API
# Fullstack CRM Uygulaması

Bu proje, Asp.Net 7.0 kullanılarak geliştirilmiş bir fullstack CRM uygulamasıdır. Uygulama, ayrı bir API tarafı ve bu API'ın sunduğu endpointleri tüketen bir frontend içermektedir.

## Teknolojiler

- **Backend:** Asp.Net API 7.0
- **Frontend:** Asp.Net MVC Html Css Bootstrap JavaScript (Borsa API endpointlerini tüketir) 
- **Veritabanı:** Microsoft SQL Server

## Kurulum

1. Veritabanını kurmak için, `.bak` dosyasını SQL Server'a geri yükleyin.
2. Gerekli veri tabanı ayarlamalarını gerçekleştirin.
3. Frontend'in API ile iletişim kurabilmesi için API ve Frontend uygulamasını beraber başlatın.

## API Kullanımı

Frontend, API'ın sunduğu endpointleri kullanarak kullanıcıya bir arayüz sunar. Ayrıca, internet üzerindeki bir borsa endpointine JavaScript aracılığıyla istek atarak bu bilgileri arayüzde kullanır.

## Postman Koleksiyonu

API endpointlerini test etmek için Postman koleksiyon dosyasını kullanabilirsiniz. 
