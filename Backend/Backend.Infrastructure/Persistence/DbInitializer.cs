using Backend.Domain.Catalog.Entities;
using Backend.Domain.Catalog.ValueObjects;
using Backend.Domain.Shared;

namespace Backend.Infrastructure.Persistence;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext db)
    {
        if (db.Brands.Any()) return; // уже засеяно

        var apple = new Brand("Apple"); 
        var samsung = new Brand("Samsung");
        var sony = new Brand("Sony");
        var xiaomi = new Brand("Xiaomi");
        var asus = new Brand("ASUS");
        var lenovo = new Brand("Lenovo");
        var hp = new Brand("HP");
        var dell = new Brand("Dell");
        var acer = new Brand("Acer");
        var msi = new Brand("MSI");
        var jbl = new Brand("JBL");
        var sennheiser = new Brand("Sennheiser");
        var google = new Brand("Google");
        var lg = new Brand("LG");

        db.Brands.AddRange(apple, samsung, sony, xiaomi, asus, lenovo, hp, dell, acer, msi, jbl, sennheiser, google, lg);
        db.SaveChanges();

        var tvs = new Category("Телевизоры", "ТВ и мониторы", "/images/homepage/1.jpg");
        var laptops = new Category("Ноутбуки", "Все ноутбуки", "/images/homepage/2.webp");
        var computers = new Category("Компьютеры", "ПК и комплектующие", "/images/homepage/3.jpg");
        var phones = new Category("Смартфоны", "Смартфоны", "/images/homepage/4.png");
        var speakers = new Category("Колонки", "Аудио", "/images/homepage/5.webp");

        db.Categories.AddRange(laptops, computers, phones, tvs, speakers);
        db.SaveChanges();

        var laptop = new ProductType("Ноутбук");
        var pc = new ProductType("ПК");
        var phone = new ProductType("Смартфон");
        var tv = new ProductType("Телевизор");
        var speaker = new ProductType("Колонка");

        db.Types.AddRange(laptop, pc, phone, tv, speaker);
        db.SaveChanges();

        var products = new List<Product>();

        var p1 = new Product(ProductName.Create("Apple MacBook Air M2"), Money.Rub(119999),
            apple.Id, laptops.Id, laptop.Id, "Ноутбук Apple MacBook Air M2");
        p1.AddImage("/images/laptop/Apple MacBook Air M2.webp", "MacBook Air M2", isMain: true);
        products.Add(p1);

        var p2 = new Product(ProductName.Create("ASUS VivoBook 15"), Money.Rub(54999),
            asus.Id, laptops.Id, laptop.Id, "Ноутбук ASUS VivoBook 15");
        p2.AddImage("/images/laptop/ASUS VivoBook 15.webp", "ASUS VivoBook 15", isMain: true);
        products.Add(p2);

        var p3 = new Product(ProductName.Create("Lenovo IdeaPad 3"), Money.Rub(44999),
            lenovo.Id, laptops.Id, laptop.Id, "Ноутбук Lenovo IdeaPad 3");
        p3.AddImage("/images/laptop/Lenovo IdeaPad 3.webp", "Lenovo IdeaPad 3", isMain: true);
        products.Add(p3);

        var p4 = new Product(ProductName.Create("Xiaomi RedmiBook 15"), Money.Rub(39999),
            xiaomi.Id, laptops.Id, laptop.Id, "Ноутбук Xiaomi RedmiBook 15");
        p4.AddImage("/images/laptop/Xiaomi RedmiBook 15.webp", "RedmiBook 15", isMain: true);
        products.Add(p4);

        var p5 = new Product(ProductName.Create("ASUS TUF Gaming"), Money.Rub(79999),
            asus.Id, laptops.Id, laptop.Id, "Игровой ноутбук ASUS TUF");
        p5.AddImage("/images/laptop/ASUS TUF Gaming.webp", "ASUS TUF Gaming", isMain: true);
        products.Add(p5);

        var p6 = new Product(ProductName.Create("ASUS ROG Strix G16"), Money.Rub(129999),
            asus.Id, laptops.Id, laptop.Id, "Игровой ноутбук, RTX 4060, i7-13650HX, 16GB DDR5, 512GB SSD");
        p6.AddImage("/images/laptop/ASUS ROG Strix G16.webp", "ASUS ROG Strix G16", isMain: true);
        products.Add(p6);

        var p7 = new Product(ProductName.Create("Lenovo IdeaPad 5 Pro"), Money.Rub(89999),
            lenovo.Id, laptops.Id, laptop.Id, "Ультрабук, Ryzen 7 7840HS, 16GB, 1TB SSD, 16\" 2.5K");
        p7.AddImage("/images/laptop/Lenovo IdeaPad 5 Pro.webp", "IdeaPad 5 Pro", isMain: true);
        products.Add(p7);

        var p8 = new Product(ProductName.Create("Apple MacBook Air M3"), Money.Rub(149999),
            apple.Id, laptops.Id, laptop.Id, "13.6\" Liquid Retina, M3, 8GB, 256GB SSD, macOS");
        p8.AddImage("/images/laptop/Apple MacBook Air M3.webp", "MacBook Air M3", isMain: true);
        products.Add(p8);

        var p9 = new Product(ProductName.Create("Xiaomi RedmiBook Pro 15"), Money.Rub(74999),
            xiaomi.Id, laptops.Id, laptop.Id, "Ryzen 7 6800H, 16GB, 512GB SSD, 15.6\" 3.2K 90Hz");
        p9.AddImage("/images/laptop/Xiaomi RedmiBook Pro 15.webp", "RedmiBook Pro 15", isMain: true);
        products.Add(p9);

        var p10 = new Product(ProductName.Create("HP Victus 16"), Money.Rub(94999),
            hp.Id, laptops.Id, laptop.Id, "Игровой ноутбук, RTX 3050, i5-12500H, 16GB, 512GB SSD");
        p10.AddImage("/images/laptop/HP Victus 16.webp", "HP Victus 16", isMain: true);
        products.Add(p10);

        var p11 = new Product(ProductName.Create("Игровой компьютер MSI"), Money.Rub(89999),
            msi.Id, computers.Id, pc.Id, "Игровой компьютер RGB");
        p11.AddImage("/images/computer/Игровой компьютер MSI.webp", "MSI Gaming", isMain: true);
        products.Add(p11);

        var p12 = new Product(ProductName.Create("Офисный компьютер HP"), Money.Rub(44999),
            hp.Id, computers.Id, pc.Id, "Офисный компьютер");
        p12.AddImage("/images/computer/Офисный компьютер HP.webp", "HP Office", isMain: true);
        products.Add(p12);

        var p13 = new Product(ProductName.Create("Домашний компьютер Dell"), Money.Rub(54999),
            dell.Id, computers.Id, pc.Id, "Домашний компьютер");
        p13.AddImage("/images/computer/Домашний компьютер Dell.jpg", "Dell Home", isMain: true);
        products.Add(p13);

        var p14 = new Product(ProductName.Create("Игровой компьютер ASUS"), Money.Rub(109999),
            asus.Id, computers.Id, pc.Id, "Компьютер ASUS ROG");
        p14.AddImage("/images/computer/Игровой компьютер ASUS.webp", "ASUS ROG PC", isMain: true);
        products.Add(p14);

        var p15 = new Product(ProductName.Create("Бюджетный компьютер Lenovo"), Money.Rub(34999),
            lenovo.Id, computers.Id, pc.Id, "Бюджетный компьютер");
        p15.AddImage("/images/computer/Бюджетный компьютер Lenovo.webp", "Lenovo Budget", isMain: true);
        products.Add(p15);

        var p16 = new Product(ProductName.Create("Игровой ПК MSI Infinite RS"), Money.Rub(189999),
            msi.Id, computers.Id, pc.Id, "RTX 4070 Ti, i7-13700KF, 32GB DDR5, 1TB NVMe");
        p16.AddImage("/images/computer/Игровой ПК MSI Infinite RS.webp", "MSI Infinite RS", isMain: true);
        products.Add(p16);

        var p17 = new Product(ProductName.Create("Офисный ПК HP ProDesk 400"), Money.Rub(44999),
            hp.Id, computers.Id, pc.Id, "i5-12500, 16GB DDR4, 512GB SSD, Windows 11 Pro");
        p17.AddImage("/images/computer/Офисный ПК HP ProDesk 400.webp", "HP ProDesk 400", isMain: true);
        products.Add(p17);

        var p18 = new Product(ProductName.Create("Домашний ПК Dell XPS 8960"), Money.Rub(99999),
            dell.Id, computers.Id, pc.Id, "RTX 3060, i7-13700, 16GB, 1TB SSD");
        p18.AddImage("/images/computer/Домашний ПК Dell XPS 8960.webp", "Dell XPS 8960", isMain: true);
        products.Add(p18);

        var p19 = new Product(ProductName.Create("ASUS ROG Strix GT35"), Money.Rub(159999),
            asus.Id, computers.Id, pc.Id, "RTX 4070, Ryzen 7 7700X, 32GB DDR5, 1TB SSD");
        p19.AddImage("/images/computer/ASUS ROG Strix GT35.webp", "ASUS ROG GT35", isMain: true);
        products.Add(p19);

        var p20 = new Product(ProductName.Create("Бюджетный ПК Acer Aspire TC"), Money.Rub(34999),
            acer.Id, computers.Id, pc.Id, "i3-12100, 8GB, 256GB SSD, без видеокарты");
        p20.AddImage("/images/computer/Бюджетный ПК Acer Aspire TC.jpg", "Acer Aspire TC", isMain: true);
        products.Add(p20);

        var p21 = new Product(ProductName.Create("Samsung QE55Q80AAUXCE"), Money.Rub(79999),
            samsung.Id, tvs.Id, tv.Id, "Телевизор Samsung QLED 4K");
        p21.AddImage("/images/products/Samsung QE55Q80AAUXCE.webp", "Samsung QLED", isMain: true);
        products.Add(p21);

        var p22 = new Product(ProductName.Create("LG OLED65C24LA"), Money.Rub(149999),
            lg.Id, tvs.Id, tv.Id, "Телевизор LG OLED 4K");
        p22.AddImage("/images/products/LG OLED65C24LA.jpg", "LG OLED", isMain: true);
        products.Add(p22);

        var p23 = new Product(ProductName.Create("Sony KD-43X80K"), Money.Rub(54999),
            sony.Id, tvs.Id, tv.Id, "Телевизор Sony BRAVIA");
        p23.AddImage("/images/products/Sony KD-43X80K.png", "Sony BRAVIA", isMain: true);
        products.Add(p23);

        var p24 = new Product(ProductName.Create("Xiaomi Mi TV P1 50"), Money.Rub(39999),
            xiaomi.Id, tvs.Id, tv.Id, "Телевизор Xiaomi 4K");
        p24.AddImage("/images/products/Xiaomi Mi TV P1 50.jpg", "Xiaomi TV P1", isMain: true);
        products.Add(p24);

        var p25 = new Product(ProductName.Create("Samsung UE32T5300AUXCE"), Money.Rub(29999),
            samsung.Id, tvs.Id, tv.Id, "Телевизор Samsung HD");
        p25.AddImage("/images/products/Samsung UE32T5300AUXCE.webp", "Samsung UE32", isMain: true);
        products.Add(p25);

        var p46 = new Product(ProductName.Create("Samsung QE55Q80C"), Money.Rub(109999),
            samsung.Id, tvs.Id, tv.Id, "55\" QLED 4K, 120Hz, Smart TV Tizen");
        p46.AddImage("/images/products/Samsung QE55Q80C.jpg", "Samsung Q80C", isMain: true);
        products.Add(p46);

        var p47 = new Product(ProductName.Create("LG OLED55C3"), Money.Rub(149999),
            lg.Id, tvs.Id, tv.Id, "55\" OLED evo 4K, 120Hz, webOS, Dolby Vision");
        p47.AddImage("/images/products/LG OLED55C3.webp", "LG OLED C3", isMain: true);
        products.Add(p47);

        var p48 = new Product(ProductName.Create("Sony XR-55A80L"), Money.Rub(179999),
            sony.Id, tvs.Id, tv.Id, "55\" OLED 4K, Cognitive Processor XR, Google TV");
        p48.AddImage("/images/products/Sony XR-55A80L.webp", "Sony XR A80L", isMain: true);
        products.Add(p48);

        var p49 = new Product(ProductName.Create("Xiaomi TV Q2 55"), Money.Rub(59999),
            xiaomi.Id, tvs.Id, tv.Id, "55\" QLED 4K, Android TV, Dolby Vision");
        p49.AddImage("/images/products/Xiaomi TV Q2 55.webp", "Xiaomi TV Q2", isMain: true);
        products.Add(p49);

        var p50 = new Product(ProductName.Create("Samsung UE65AU7100"), Money.Rub(74999),
            samsung.Id, tvs.Id, tv.Id, "65\" LED 4K, Crystal Processor, Smart TV");
        p50.AddImage("/images/products/Samsung UE65AU7100.webp", "Samsung UE65", isMain: true);
        products.Add(p50);

        var p26 = new Product(ProductName.Create("Apple iPhone 15 Pro"), Money.Rub(119999),
            apple.Id, phones.Id, phone.Id, "Смартфон Apple iPhone 15 Pro");
        p26.AddImage("/images/phone/Apple iPhone 15 Pro.webp", "iPhone 15 Pro", isMain: true);
        products.Add(p26);

        var p27 = new Product(ProductName.Create("Samsung Galaxy S24"), Money.Rub(89999),
            samsung.Id, phones.Id, phone.Id, "Смартфон Samsung Galaxy S24");
        p27.AddImage("/images/phone/Samsung Galaxy S24.webp", "Galaxy S24", isMain: true);
        products.Add(p27);

        var p28 = new Product(ProductName.Create("Xiaomi 14 Ultra"), Money.Rub(69999),
            xiaomi.Id, phones.Id, phone.Id, "Смартфон Xiaomi 14 Ultra");
        p28.AddImage("/images/phone/Xiaomi 14 Ultra.webp", "Xiaomi 14 Ultra", isMain: true);
        products.Add(p28);

        var p29 = new Product(ProductName.Create("Google Pixel 8"), Money.Rub(64999),
            google.Id, phones.Id, phone.Id, "Смартфон Google Pixel 8");
        p29.AddImage("/images/phone/Google Pixel 8.webp", "Pixel 8", isMain: true);
        products.Add(p29);

        var p30 = new Product(ProductName.Create("Sony Xperia 1 V"), Money.Rub(79999),
            sony.Id, phones.Id, phone.Id, "Смартфон Sony Xperia 1 V");
        p30.AddImage("/images/phone/Sony Xperia 1 V.jpg", "Xperia 1 V", isMain: true);
        products.Add(p30);

        var p36 = new Product(ProductName.Create("Apple iPhone 15 Pro Max"), Money.Rub(139999),
            apple.Id, phones.Id, phone.Id, "6.7\" Super Retina XDR, A17 Pro, 256GB, титан");
        p36.AddImage("/images/phone/Apple iPhone 15 Pro Max.webp", "iPhone 15 Pro Max", isMain: true);
        products.Add(p36);

        var p37 = new Product(ProductName.Create("Samsung Galaxy S24 Ultra"), Money.Rub(129999),
            samsung.Id, phones.Id, phone.Id, "6.8\" QHD+, Snapdragon 8 Gen 3, 12GB, 256GB, S-Pen");
        p37.AddImage("/images/phone/Samsung Galaxy S24 Ultra.jpg", "Galaxy S24 Ultra", isMain: true);
        products.Add(p37);

        var p38 = new Product(ProductName.Create("Xiaomi 14 Pro"), Money.Rub(89999),
            xiaomi.Id, phones.Id, phone.Id, "6.73\" AMOLED, Snapdragon 8 Gen 3, 12GB, 512GB");
        p38.AddImage("/images/phone/Xiaomi 14 Pro.webp", "Xiaomi 14 Pro", isMain: true);
        products.Add(p38);

        var p39 = new Product(ProductName.Create("Google Pixel 8 Pro"), Money.Rub(99999),
            google.Id, phones.Id, phone.Id, "6.7\" LTPO OLED, Tensor G3, 12GB, 128GB");
        p39.AddImage("/images/phone/Google Pixel 8 Pro.jpg", "Pixel 8 Pro", isMain: true);
        products.Add(p39);

        var p40 = new Product(ProductName.Create("Sony Xperia 1 V"), Money.Rub(109999),
            sony.Id, phones.Id, phone.Id, "6.5\" 4K OLED, Snapdragon 8 Gen 2, 12GB, 256GB");
        p40.AddImage("/images/phone/Sony Xperia 1 Vblack.webp", "Xperia 1 V Black", isMain: true);
        products.Add(p40);

        var p31 = new Product(ProductName.Create("JBL Charge 5"), Money.Rub(12999),
            jbl.Id, speakers.Id, speaker.Id, "Колонка JBL Charge 5");
        p31.AddImage("/images/speaker/JBL Charge 5.webp", "JBL Charge 5", isMain: true);
        products.Add(p31);

        var p32 = new Product(ProductName.Create("JBL Flip 6"), Money.Rub(9999),
            jbl.Id, speakers.Id, speaker.Id, "Колонка JBL Flip 6");
        p32.AddImage("/images/speaker/JBL Flip 6.webp", "JBL Flip 6", isMain: true);
        products.Add(p32);

        var p33 = new Product(ProductName.Create("Sony SRS-XB43"), Money.Rub(14999),
            sony.Id, speakers.Id, speaker.Id, "Колонка Sony SRS-XB43");
        p33.AddImage("/images/speaker/Sony SRS-XB43.jpg", "Sony SRS-XB43", isMain: true);
        products.Add(p33);

        var p34 = new Product(ProductName.Create("Sennheiser AMBEO"), Money.Rub(29999),
            sennheiser.Id, speakers.Id, speaker.Id, "Колонка Sennheiser AMBEO");
        p34.AddImage("/images/speaker/Sennheiser AMBEO.webp", "Sennheiser AMBEO", isMain: true);
        products.Add(p34);

        var p35 = new Product(ProductName.Create("Xiaomi Mi Portable"), Money.Rub(3999),
            xiaomi.Id, speakers.Id, speaker.Id, "Колонка Xiaomi Mi Portable");
        p35.AddImage("/images/speaker/Xiaomi Mi Portable.webp", "Xiaomi Mi Portable", isMain: true);
        products.Add(p35);

        var p41 = new Product(ProductName.Create("JBL Charge 5"), Money.Rub(17999),
            jbl.Id, speakers.Id, speaker.Id, "Портативная колонка, 40Вт, IP67, 20ч работы");
        p41.AddImage("/images/speaker/JBL Charge 5white.webp", "JBL Charge 5 White", isMain: true);
        products.Add(p41);

        var p42 = new Product(ProductName.Create("JBL Flip 6"), Money.Rub(12999),
            jbl.Id, speakers.Id, speaker.Id, "Портативная колонка, 30Вт, IP67, 12ч работы");
        p42.AddImage("/images/speaker/JBL Flip6new.webp", "JBL Flip 6 New", isMain: true);
        products.Add(p42);

        var p43 = new Product(ProductName.Create("Sony SRS-XB43"), Money.Rub(19999),
            sony.Id, speakers.Id, speaker.Id, "Портативная колонка, Extra Bass, IP67, 24ч");
        p43.AddImage("/images/speaker/Sony SRS-XB43new.jpg", "Sony SRS-XB43 New", isMain: true);
        products.Add(p43);

        var p44 = new Product(ProductName.Create("Sennheiser Momentum 4"), Money.Rub(34999),
            sennheiser.Id, speakers.Id, speaker.Id, "Беспроводная колонка, Hi-Fi звук, 60Вт");
        p44.AddImage("/images/speaker/Sennheiser Momentum 4.webp", "Momentum 4", isMain: true);
        products.Add(p44);

        var p45 = new Product(ProductName.Create("Xiaomi Mi Sound Pro"), Money.Rub(9999),
            xiaomi.Id, speakers.Id, speaker.Id, "Умная колонка, 30Вт, Bluetooth 5.0, голосовой помощник");
        p45.AddImage("/images/speaker/Xiaomi Mi Sound Pro.jpg", "Mi Sound Pro", isMain: true);
        products.Add(p45);

        db.Products.AddRange(products);
        db.SaveChanges();
    }
}