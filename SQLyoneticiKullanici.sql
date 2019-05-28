--YöneticiKullaniciGirisi
create database personel
use personel

create table kullanicilar(
tcNo nvarchar(11) not null,
ad nvarchar(30) not null,
soyad nvarchar(30) not null,
yetki nvarchar(30) not null,
kullaniciAdi nvarchar(30) not null,
parola nvarchar(30) not null
)

insert into kullanicilar values ('11111111111','Ahmet','Can','Yönetici','ahmtcn','+Ahmet25+'),
('22222222222','Mehmet','Yýlmaz','Kullanýcý','mhmt18','Mehmet+-20')

select * from kullanicilar
delete from kullanicilar