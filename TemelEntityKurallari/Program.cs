﻿using System;
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
            #endregion

            #region SaveChanges nedir ?
            // Bu şekilde eklenebildiği gibi; Bu tip güvensiz ekleme yöntemidir.
            //await context.AddAsync(product1);
            //// Bu şekilde de eklenebilir; Bu tip güvenli ekleme yöntemidir.
            //Console.WriteLine(context.Entry(product).State);
            //await context.Products.AddAsync(product);
            //context.SaveChanges();
            //Insert, update ve delete sorgularını oluşturup bir transaction eşliğinde veritabanına gönderip execute eden fonskiyondur.Eğer ki oluşturulan
            //sorgulardan herhangi birisi başarısız olursa tüm işlemleri geri alır(rollback).
            #endregion

            #region Ef Core açısından bir verinin eklenmesi gerektiği nasıl anlaşılıyor?
            // context.SaveChanges(), bir işlemin insert mi, update mi, delete mi olup olmadığını nasıl anlıyor? Veya ne işlem yaptığını biz nasıl anlarız?
            //Console.WriteLine(context.Entry(product).State);
            #endregion

            #region Birden Fazla Veri Eklenirken Nelere Dikkat Etmeliyiz?
            // Transaction bi maliyettir.
            ECommerceContext context = new();
            Product product1 = new()
            {
                ProductName = "A ürünü",
                UnitPrice = 50.5
            };
            Product product2 = new()
            {
                ProductName = "B ürünü",
                UnitPrice = 20.5
            };
            Product product3 = new()
            {
                ProductName = "C ürünü",
                UnitPrice = 25.54
            };
            Console.WriteLine(context.Entry(product1));

            // TEK TEK ekledik
            //await context.AddAsync(product1); 
            //await context.AddAsync(product2);
            //await context.AddAsync(product3);

            await context.Products.AddRangeAsync(product1, product2, product3); // Hepsini birden ekledik.
            await context.SaveChangesAsync();
            //Savechanges fonksiyonu her tetiklendiğinde bir transaction oluşturacağından dolayı Ef core ile yapılan her bir işleme özel kullanmaktan kaçınmalıyız! Çünkü her işleme
            // özel transaction veritabanı açısından ekstra maliyet demektir. O yüzden mümkün mertebe tüm işlerimizi tek bir transaction eşliğinde veritabanına gönderebilmek için
            // savechanges'i aşağıdaki gibi tek seferde kullanmak hem maliyet hem de yönetilebilebilirlik açısından katkıda bulunmuş 


            Console.WriteLine(context.Entry(product1)); // Her bir işleme özel sorguyu tek bir transactionda oluşturup bunu veritabanına gönderir.

            
            #endregion


        }
    }
}
