# OData
## Open Data Protocol
<strong>OData:</strong> Veri kaynaklarımızı url üzerinden sorgulamamıza imkan veren protokoldür. <br>
<strong>Protokol:</strong> İki tane farklı sistemin birbiriyle nasıl haberleşeceğini belirleyen kurallar bütünüdür.<br>
Odata entityler üzerinde sorgulama işlemi yapar. <br>
<strong>Navigation Property:</strong> Navigation Property bir entity ile başka bir entity arasında olan ilişkiyi temsil eder.<br>
Query Option:Bir serverdan dönecek data miktarını kontrol etmemize imkan veren seçeneklerdir.<br>
https://www.myapi.com/data/products?$select=Id,Name...,... (Id ve Name getirir.)<br>
products?$expand=category   (productların ilişkili olduğu kategorileri getirir.)<br>
products?$top=10   (10 tane product getirir.)<br>
products?$skip=2 (ilk 2 taneyi atlar 3.den itibaren getirir.)<br>
Skip ve top aynı anda kullanırsak sayfalama özelliği kazandırırız.<br>
products?$count=true Dönen datanın sayısını bize bildirir.<br>
products?$filter=name eq 'Kalem' Kalem'e eşit olanları getir.<br>
products?$filter=name ne 'Kalem' Kalem'e eşit olmayanları getir.<br>
products?$filter=Stock ge 100 Stok sayısı 100 e eşit olanları ve 100 den büyük olanları getirir.<br>
products?$filter=Stock lt 100 Stok sayısı 100 e eşit olanları ve 100 den küçük olanları getirir.<br>
## Tarih Sorguları
https://localhost:44307/odata/Products?$filter=Created gt 2006-02-02
https://localhost:44307/odata/Products?$filter=Created lt 2006-02-02
products?$filter=year(tarih) eq 2005 <br>
<strong>IQueryable:</strong> Veri tabanında where ifadesi ile birlikte ilgili data seçilir. <br>
<strong>IEnumerable:</strong>  Sql serverdan tüm datayı alır. <br>
## Custom İsimlendirme
  [ODataRoute("Products({Item})")] <br>
        [EnableQuery] <br>
        public IActionResult GetUrun([FromODataUri]int item)<br
        {<br>
            return Ok(_context.Products.Where(x => x.Id == item));<br>
        }<br>
## Custom Route Yazmak <br>
Default Path <br>
https://localhost:44306/odata/categories(1)?$expand=products<br>
Yeni Path<br>
https://localhost:44306/odata/categories(2)/products(2)<br>
## Sayfalama
[EnableQuery(PageSize=2)] 
## ODATA CREATE
[HttpPost] <br>
        public IActionResult Post([FromBody]Product product)<br>
        {<br>
            _context.Products.Add(product);<br>
            _context.SaveChanges();<br>
            return Ok(product);<br>
        }<br>
## ODATA UPDATE
[HttpPut] <br>
        public IActionResult PutProduct([FromODataUri]int key,[FromBody] Product product)<br>
        {<br>
            product.Id = key;<br>
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;<br>
            _context.SaveChanges();<br>
            return NoContent();<br>
        }<br>
