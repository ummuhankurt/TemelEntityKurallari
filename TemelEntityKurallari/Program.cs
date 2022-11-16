using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemelEntityKurallari
{
    class Program
    {
        static async Task Main(string[] args)
        {

            #region Veri Nasıl Eklenir?
            //ECommerceContext context = new();
            //Product product1 = new()
            //{
            //    ProductName = "Chai",
            //    UnitPrice = 54.5
            //};
            //Product product = new()
            //{
            //    ProductName = "A ürünü",
            //    UnitPrice = 20.99
            //};
            //await context.AddAsync(product1);
            //await context.Products.AddAsync(product);
            //await context.SaveChangesAsync();
            #endregion

            #region SaveChanges nedir 
            //Insert, update ve delete sorgularını oluşturup bir transaction eşliğinde veritabanına gönderip execute eden fonskiyondur.Eğer ki oluşturulan
            //sorgulardan herhangi birisi başarısız olursa tüm işlemleri geri alır(rollback).
            #endregion

            #region Ef Core açısından bir verinin eklenmesi gerektiği nasıl anlaşılıyor?
            // context.SaveChanges(), bir işlemin insert mi, update mi, delete mi olup olmadığını nasıl anlıyor? Veya ne işlem yaptığını biz nasıl anlarız?
            //Console.WriteLine(context.Entry(product).State);
            #endregion

            #region Birden Fazla Veri Eklenirken Nelere Dikkat Etmeliyiz?
            // Transaction bi maliyettir.
            //ECommerceContext context = new();
            //Product product1 = new()
            //{
            //    ProductName = "A ürünü",
            //    UnitPrice = 50.5
            //};
            //Product product2 = new()
            //{
            //    ProductName = "B ürünü",
            //    UnitPrice = 20.5
            //};
            //Product product3 = new()
            //{
            //    ProductName = "C ürünü",
            //    UnitPrice = 25.54
            //};

            // TEK TEK ekledik
            //await context.AddAsync(product1); 
            //await context.AddAsync(product2);
            //await context.AddAsync(product3);

            //await context.Products.AddRangeAsync(product1, product2, product3); // Hepsini birden ekledik.
            //await context.SaveChangesAsync();
            //Savechanges fonksiyonu her tetiklendiğinde bir transaction oluşturacağından dolayı Ef core ile yapılan her bir işleme özel kullanmaktan kaçınmalıyız! Çünkü her işleme
            // özel transaction veritabanı açısından ekstra maliyet demektir. O yüzden mümkün mertebe tüm işlerimizi tek bir transaction eşliğinde veritabanına gönderebilmek için
            // savechanges'i aşağıdaki gibi tek seferde kullanmak hem maliyet hem de yönetilebilebilirlik açısından katkıda bulunmuş 
            // Her bir işleme özel sorguyu tek bir transactionda oluşturup bunu veritabanına gönderir.


            #endregion

            #region Veri nasıl Güncellenir?
            // Güncellenecek veri önce veri tabanından çağırılır.
            //ECommerceContext context = new();
            //Product product =  await context.Products.FirstOrDefaultAsync(p => p.ProductId == 3);
            //product.ProductName = "C ürünü güncellendi";
            //product.UnitPrice = 999;
            //await context.SaveChangesAsync();
            //Console.WriteLine(product.ProductName);
            #endregion

            #region ChangeTracker Nedir? Context üzerinden gelen verilerin takibinden sorumlu olan bir mekanizmadır.
            //Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemler neticesinde update yahut delete sorgularının oluşturulacağı anlaşılır!


            #endregion

            #region Takip edilmeyen veriler nasıl güncellenir? Update() fonksiyonu.
            // ChangeTracker mekanizması tarafından takip edilmeyen nesnelerin güncellenebilmesi için Update fonksiyonu kullnılır!
            // Update fonksiyonunu kullanabilmek için kesinlikle ilgili nesnede id bilgisi verilmelidir.
            // Bu değer güncellenecek(update sorgusu oluşturulacak) verinin hangisi olduğunu ifade edecektir.
            //ECommerceContext context = new();
            //Product product1 = new()
            //{
            //    ProductId = 3,
            //};
            //product1.ProductName = "C usdfdsf.";
            //context.Products.Update(product1);
            //await context.SaveChangesAsync();
            #endregion

            #region EntityState Nedir?
            // Bir entity instance'ının durumunu ifade eden bir referanstır.

            //Product product = new();
            //Console.WriteLine(context.Entry(product).State);
            #endregion

            #region Ef Core açısından bir verinin güncellenmesi gerektiği nasıl anlaşılıyor?
            //Product product = await context.Products.FirstOrDefaultAsync(p => p.ProductId == 3);
            //Console.WriteLine(context.Entry(product).State);
            //product.ProductName = "Hilmi";
            //Console.WriteLine(context.Entry(product).State);
            //await context.SaveChangesAsync();
            #endregion

            #region Birden fazla veri güncellenirken nelere dikkat edilmelidir?

            //var products = await context.Products.ToListAsync(); // chanfe tracker takip eder
            //foreach (var item in products)
            //{
            //    item.ProductName += "*";
            //}
            //await context.SaveChangesAsync();
            //foreach (var item in products)
            //{
            //    Console.WriteLine(item.ProductName);
            //}
            #endregion

            #region Veri Nasıl Silinir?
            //Product product = await context.Products.SingleOrDefaultAsync(p => p.ProductId == 3);
            //context.Products.Remove(product);
            //await context.SaveChangesAsync();
            // Veri context'ten gelmezse?
            //ECommerceContext context = new();
            //Product product = new()
            //{
            //    ProductId = 2
            //};
            //context.Products.Remove(product);
            //await context.SaveChangesAsync();

            #endregion
            #region EntityState ile Silme İşlemi
            //ECommerceContext context = new();
            //Product product = new()
            //{ ProductId = 4 };
            //context.Entry(product).State = EntityState.Deleted;
            //await context.SaveChangesAsync();
            #endregion
            #region Birden fazla veri silinirken nelere dikkat edilmeli?
            // Ne kadar veri sileceksen sil, savechanges'i bir defa çalıştır. Savechanges'i verimli kullan.
            #endregion
            #region RemoveRange
            //ECommerceContext context = new();
            //List<Product> products =  context.Products.Where(p => p.ProductId >= 9 && p.ProductId <= 11).ToList();
            //context.Products.RemoveRange(products);
            //await context.SaveChangesAsync();
            #endregion
        }
    }
}
