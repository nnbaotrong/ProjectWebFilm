
create database webmovie
go

use webmovie
go



create table AdminPro(
id int identity primary key,
ten_admin nvarchar(50),
mat_khau_admin nvarchar(50)
)
go

create table Banner(
id_banner int identity primary key,
ten_banner nvarchar(50),
hinh_banner nvarchar(100)
)
go


create table Loai(
id_loai int identity primary key,
ten_loai nvarchar(50)
)
go

create table QuocGia(
id_quoc_gia int identity primary key,
ten_quoc_gia nvarchar(50),
)
go

create table DienVien(
id_dien_vien int identity primary key,
ten_dien_vien nvarchar(50),
anh_bia nvarchar(100)
)
go

create table NamPhatHanh(
id_nam int identity primary key,
nam_phat_hanh int
)
go

create table TrangThai(
id_trang_thai int identity primary key,
ten_trang_thai nvarchar(50)
)
go

create table NguoiDung(
id_nguoi_dung int identity primary key,
ten_nguoi_dung nvarchar(50),
tai_khoan nvarchar(50),
mat_khau nvarchar(50),
email nvarchar(50),


)
go



create table Phim(
id_phim int identity primary key,
ten_phim nvarchar(50),
anh_bia nvarchar(100),
link_phim nvarchar(100),
mo_ta nvarchar(200),
trailer nvarchar(100),
id_trang_thai int,
id_quoc_gia int,
id_nam int,
CONSTRAINT FK_trangthai_phim FOREIGN KEY (id_trang_thai) REFERENCES TrangThai(id_trang_thai),
CONSTRAINT FK_quocgia_phim FOREIGN KEY (id_quoc_gia) REFERENCES quocgia(id_quoc_gia),
CONSTRAINT FK_namphathanh_phim FOREIGN KEY (id_nam) REFERENCES NamPhatHanh(id_nam),

)
go

create table Phim_Dienvien(
id_phim int,
id_dien_vien int,
PRIMARY KEY (id_phim,id_dien_vien),
CONSTRAINT FK_dv_phim FOREIGN KEY (id_dien_vien) REFERENCES dienvien(id_dien_vien),
CONSTRAINT FK_phim_dv FOREIGN KEY (id_phim) REFERENCES phim(id_phim)
)
go

create table ChiTietLoai(
id_phim int,
id_loai int,
PRIMARY KEY (id_phim,id_loai),
CONSTRAINT FK_loai_ctl FOREIGN KEY (id_loai) REFERENCES loai(id_loai),
CONSTRAINT FK_phim_ctl FOREIGN KEY (id_phim) REFERENCES phim(id_phim)
)
go

create table BinhLuan(
id_binh_luan int identity primary key,
id_nguoi_dung int,
id_phim int,
noi_dung nvarchar(200),
ngay_binh_luan date,
CONSTRAINT FK_phim_binhloan FOREIGN KEY (id_phim) REFERENCES Phim(id_phim),
CONSTRAINT FK_nd_binhluan FOREIGN KEY (id_nguoi_dung) REFERENCES NguoiDung(id_nguoi_dung)
)
go



create table DanhGia(
id_nguoi_dung int,
id_phim int,
diem int,
tong_diem int,
PRIMARY KEY (id_phim,id_nguoi_dung),
CONSTRAINT FK_kh_danhgia FOREIGN KEY (id_phim) REFERENCES phim(id_phim),
CONSTRAINT FK_phim_danhgia FOREIGN KEY (id_nguoi_dung) REFERENCES NguoiDung(id_nguoi_dung)
)
go


delete Banner
DBCC CHECKIDENT (Banner, RESEED, 0)
DBCC CHECKIDENT (Banner)
insert into Banner(ten_banner,hinh_banner) values (N'Thor 3',N'banner3.jpg')
insert into Banner(ten_banner,hinh_banner) values (N'Avenger: EndGame',N'banner1.jpg')
insert into Banner(ten_banner,hinh_banner) values (N'Spiderman: No Way Home',N'banner2.jpg')


delete Loai
DBCC CHECKIDENT (Loai, RESEED, 0)
DBCC CHECKIDENT (Loai)
insert into loai values ('Phiêu Lưu'),(N'Hành Động'),(N'Nhập Vai'),(N'Viễn Tưởng'),(N'Kiếm Hiệp'),(N'Không Gian')



delete NamPhatHanh
DBCC CHECKIDENT (NamPhatHanh, RESEED, 0)
DBCC CHECKIDENT (NamPhatHanh)
insert into NamPhatHanh(nam_phat_hanh) values (2000)
insert into NamPhatHanh(nam_phat_hanh) values (2001)
insert into NamPhatHanh(nam_phat_hanh) values (2002)
insert into NamPhatHanh(nam_phat_hanh) values (2003)
insert into NamPhatHanh(nam_phat_hanh) values (2004)
insert into NamPhatHanh(nam_phat_hanh) values (2005)
insert into NamPhatHanh(nam_phat_hanh) values (2006)
insert into NamPhatHanh(nam_phat_hanh) values (2007)
insert into NamPhatHanh(nam_phat_hanh) values (2008)
insert into NamPhatHanh(nam_phat_hanh) values (2009)
insert into NamPhatHanh(nam_phat_hanh) values (2010)

delete QuocGia
DBCC CHECKIDENT (QuocGia, RESEED, 0)
DBCC CHECKIDENT (QuocGia)
insert into QuocGia(ten_quoc_gia) values (N'Việt Nam')
insert into QuocGia(ten_quoc_gia) values (N'Trung Quốc')
insert into QuocGia(ten_quoc_gia) values (N'Hàn Quốc')
insert into QuocGia(ten_quoc_gia) values (N'Nhật Bản')
insert into QuocGia(ten_quoc_gia) values (N'Thái Lan')


delete DienVien
DBCC CHECKIDENT (DienVien, RESEED, 0)
DBCC CHECKIDENT (DienVien)
insert into DienVien values (N'Châu Tinh Trì',N'dienvien1.jpg')
insert into DienVien values (N'Diệp Vấn',N'dienvien2.jpg')
insert into DienVien values (N'Lý Tiểu Long',N'dienvien3.jpg')


insert into DienVien values (N'Việt An',N'dienvien2.jpg')
insert into DienVien values (N'Bảo Trọng',N'dienvien3.jpg')
insert into DienVien values (N'Hoài Rin',N'dienvien4.jpg')

delete TrangThai
DBCC CHECKIDENT (TrangThai, RESEED, 0)
DBCC CHECKIDENT (TrangThai)
insert into TrangThai values (N'Đã Phát Hành')
insert into TrangThai values (N'Chưa Phát Hành')

delete NguoiDung
DBCC CHECKIDENT (NguoiDung, RESEED, 0)
DBCC CHECKIDENT (NguoiDung)
insert into NguoiDung values (N'Nguyễn Ngọc Bảo Trọng',N'nnbaotrong',N'123','nnbaotrong@gmail.com')
insert into NguoiDung values (N'Phạm Việt An',N'viet123',N'123','viet123g@gmail.com')


delete AdminPro
DBCC CHECKIDENT (AdminPro, RESEED, 0)
DBCC CHECKIDENT (AdminPro)
insert into AdminPro values (N'admin',N'123')

delete Phim
DBCC CHECKIDENT (Phim, RESEED, 0)
DBCC CHECKIDENT (Phim)
insert into Phim values (N'Iron Man',N'Iron Man.jpg','https://www.youtube.com/embed/8ugaeA-nMTc',N'Phim Hay vler','https://www.youtube.com/embed/8ugaeA-nMTc',1,1,1)
insert into Phim values (N'Iron Man 2',N'Iron Man 2.jpg','https://www.youtube.com/embed/BoohRoVA9WQ',N'Phim Hay vler','https://www.youtube.com/embed/BoohRoVA9WQ',1,5,2)
insert into Phim values (N'Iron Man 3',N'Iron Man 3.jpg','https://www.youtube.com/embed/Ke1Y3P9D0Bc',N'Phim Hay vler','https://www.youtube.com/embed/Ke1Y3P9D0Bc',1,4,1)
insert into Phim values (N'Captain America 2',N'Captain America 2.jpg','https://www.youtube.com/embed/7SlILk2WMTI',N'Phim Hay vler','https://www.youtube.com/embed/7SlILk2WMTI',1,2,1)

insert into Phim values (N'Thor',N'Thor.jpg','https://www.youtube.com/embed/JOddp-nlNvQ',N'Phim Hay vler','https://www.youtube.com/embed/fHkqGm4mnCQ',1,2,3)
insert into Phim values (N'Thor The Dark World',N'Thor The Dark World.jpg','https://www.youtube.com/embed/npvJ9FTgZbM',N'Phim Hay vler','https://www.youtube.com/embed/kk93l8ofr-g',2,4,5)
insert into Phim values (N'Captain America',N'Captain America.jpg','https://www.youtube.com/embed/eyvg5D_aedc',N'Phim Hay vler','https://www.youtube.com/embed/eyvg5D_aedc',1,2,1)
insert into Phim values (N'The Incredible Hulk',N'The Incredible Hulk.jpg','https://www.youtube.com/embed/rOLzEzTQhNM',N'Phim Hay vler','https://www.youtube.com/embed/rOLzEzTQhNM',1,4,8)
insert into Phim values (N'Marvels Ant-Man',N'Ant-Man.jpg','https://www.youtube.com/embed/pWdKf3MneyI',N'Phim Hay vler','https://www.youtube.com/embed/pWdKf3MneyI',2,1,1)
insert into Phim values (N'Doctor Strange',N'Doctor Strange.jpg','https://www.youtube.com/embed/Lt-U_t2pUHI',N'Phim Hay vler','https://www.youtube.com/embed/Lt-U_t2pUHI',1,2,2)
insert into Phim values (N'Black Panther',N'Black Panther.jpg','https://www.youtube.com/embed/xjDjIWPwcPU',N'Phim Hay vler','https://www.youtube.com/embed/xjDjIWPwcPU',2,3,4)
insert into Phim values (N'Black Widow',N'Black Widow.jpg','https://www.youtube.com/embed/Fp9pNPdNwjI',N'Phim Hay vler','https://www.youtube.com/embed/Fp9pNPdNwjI',1,1,5)
insert into Phim values (N'Captain America Civil War ',N'Captain America Civil War.jpg','https://www.youtube.com/embed/UUkn-enk2RU',N'Phim Hay vler','https://www.youtube.com/embed/UUkn-enk2RU',2,2,6)
insert into Phim values (N'Captain Marvel',N'Captain Marvel.jpg','https://www.youtube.com/embed/Z1BCujX3pw8',N'Phim Hay vler','https://www.youtube.com/embed/Z1BCujX3pw8',1,1,5)
insert into Phim values (N'Doctor Strange 2',N'Doctor Strange in the Multiverse of Madness.jpg','https://www.youtube.com/embed/vKrFf7fyIfY',N'Phim Hay vler','https://www.youtube.com/embed/vKrFf7fyIfY',1,1,5)
insert into Phim values (N'Guardians of the Galaxy 2',N'Guardians of the Galaxy 2.jpg','https://www.youtube.com/embed/dW1BIid8Osg',N'Phim Hay vler','https://www.youtube.com/embed/dW1BIid8Osg',1,1,5)
insert into Phim values (N'Guardians of the Galaxy',N'Guardians of the Galaxy.jpg','https://www.youtube.com/embed/d96cjJhvlMA',N'Phim Hay vler','https://www.youtube.com/embed/d96cjJhvlMA',1,1,5)
insert into Phim values (N'Shang-Chi',N'Shang-Chi and the Legend of the Ten Rings.jpg','https://www.youtube.com/embed/8YjFbMbfXaQ',N'Phim Hay vler','https://www.youtube.com/embed/8YjFbMbfXaQ',1,1,5)
insert into Phim values (N'Spider-Man Far From Home',N'Spider-Man Far From Home.jpg','https://www.youtube.com/embed/Nt9L1jCKGnE',N'Phim Hay vler','https://www.youtube.com/embed/Nt9L1jCKGnE',1,1,5)
insert into Phim values (N'Spider-Man Homecoming',N'Spider-Man Homecoming.jpg','https://www.youtube.com/embed/n9DwoQ7HWvI',N'Phim Hay vler','https://www.youtube.com/embed/n9DwoQ7HWvI',1,1,5)
insert into Phim values (N'The Avengers',N'The Avengers.jpg','https://www.youtube.com/embed/eOrNdBpGMv8',N'Phim Hay vler','https://www.youtube.com/embed/eOrNdBpGMv8',1,1,5)
insert into Phim values (N'The Eternals',N'The Eternals.jpg','https://www.youtube.com/embed/DobBbl0_6Lc',N'Phim Hay vler','https://www.youtube.com/embed/DobBbl0_6Lc',1,1,5)
insert into Phim values (N'Falcon and Winter Soldier',N'The Falcon and the Winter Soldier.jpg','https://www.youtube.com/embed/IWBsDaFWyTE',N'Phim Hay vler','https://www.youtube.com/embed/IWBsDaFWyTE',1,1,5)
insert into Phim values (N'Thor Love and Thunder',N'Thor Love and Thunder.jpg','https://www.youtube.com/embed/eK_LB60jmnA',N'Phim Hay vler','https://www.youtube.com/embed/eK_LB60jmnA',1,1,5)
insert into Phim values (N'Thor Ragnarok',N'Thor Ragnarok.jpg','https://www.youtube.com/embed/eK_LB60jmnA',N'Phim Hay vler','https://www.youtube.com/embed/eK_LB60jmnA',1,1,5)

delete ChiTietLoai
insert into ChiTietLoai values (1,1)
insert into ChiTietLoai values (1,3)
insert into ChiTietLoai values (1,2)
insert into ChiTietLoai values (2,2)
insert into ChiTietLoai values (2,3)
insert into ChiTietLoai values (3,3)
insert into ChiTietLoai values (3,1)

insert into ChiTietLoai values (4,1)
insert into ChiTietLoai values (4,5)
insert into ChiTietLoai values (4,3)
insert into ChiTietLoai values (5,6)
insert into ChiTietLoai values (5,4)
insert into ChiTietLoai values (6,4)
insert into ChiTietLoai values (6,2)

insert into ChiTietLoai values (7,1)
insert into ChiTietLoai values (7,5)
insert into ChiTietLoai values (8,3)
insert into ChiTietLoai values (8,6)
insert into ChiTietLoai values (9,1)
insert into ChiTietLoai values (9,2)
insert into ChiTietLoai values (10,2)

insert into ChiTietLoai values (10,1)
insert into ChiTietLoai values (11,5)
insert into ChiTietLoai values (11,3)
insert into ChiTietLoai values (12,5)
insert into ChiTietLoai values (12,3)
insert into ChiTietLoai values (13,6)
insert into ChiTietLoai values (13,4)
insert into ChiTietLoai values (14,1)
insert into ChiTietLoai values (14,2)

insert into ChiTietLoai values (15,2)
insert into ChiTietLoai values (15,1)
insert into ChiTietLoai values (16,3)
insert into ChiTietLoai values (16,2)
insert into ChiTietLoai values (17,1)
insert into ChiTietLoai values (17,2)
insert into ChiTietLoai values (18,4)
insert into ChiTietLoai values (18,5)
insert into ChiTietLoai values (19,2)

insert into ChiTietLoai values (20,3)
insert into ChiTietLoai values (20,2)
insert into ChiTietLoai values (21,3)
insert into ChiTietLoai values (21,5)
insert into ChiTietLoai values (22,1)
insert into ChiTietLoai values (22,5)
insert into ChiTietLoai values (23,4)
insert into ChiTietLoai values (23,5)
insert into ChiTietLoai values (24,2)
insert into ChiTietLoai values (24,1)

insert into ChiTietLoai values (25,5)
insert into ChiTietLoai values (25,2)

delete Phim_Dienvien
insert into Phim_Dienvien values (1,1)
insert into Phim_Dienvien values (1,2)
insert into Phim_Dienvien values (2,1)
insert into Phim_Dienvien values (2,3)
insert into Phim_Dienvien values (3,3)

insert into Phim_Dienvien values (1,1)
insert into Phim_Dienvien values (1,2)
insert into Phim_Dienvien values (2,1)
insert into Phim_Dienvien values (2,3)
insert into Phim_Dienvien values (3,3)

insert into Phim_Dienvien values (4,3)
insert into Phim_Dienvien values (4,2)
insert into Phim_Dienvien values (5,4)
insert into Phim_Dienvien values (5,5)
insert into Phim_Dienvien values (6,5)

insert into Phim_Dienvien values (7,1)
insert into Phim_Dienvien values (7,2)
insert into Phim_Dienvien values (8,1)
insert into Phim_Dienvien values (8,3)
insert into Phim_Dienvien values (9,3)

insert into Phim_Dienvien values (10,3)
insert into Phim_Dienvien values (11,2)
insert into Phim_Dienvien values (11,4)
insert into Phim_Dienvien values (12,5)
insert into Phim_Dienvien values (12,1)

insert into Phim_Dienvien values (13,1)
insert into Phim_Dienvien values (13,2)
insert into Phim_Dienvien values (14,1)
insert into Phim_Dienvien values (15,3)
insert into Phim_Dienvien values (16,3)

insert into Phim_Dienvien values (17,3)
insert into Phim_Dienvien values (18,2)
insert into Phim_Dienvien values (18,4)
insert into Phim_Dienvien values (19,5)
insert into Phim_Dienvien values (19,1)

insert into Phim_Dienvien values (20,6)
insert into Phim_Dienvien values (21,5)
insert into Phim_Dienvien values (22,4)
insert into Phim_Dienvien values (23,3)
insert into Phim_Dienvien values (23,4)

insert into Phim_Dienvien values (24,6)
insert into Phim_Dienvien values (24,5)
insert into Phim_Dienvien values (25,4)
insert into Phim_Dienvien values (25,5)

delete BinhLuan
DBCC CHECKIDENT (BinhLuan, RESEED, 0)
DBCC CHECKIDENT (BinhLuan)
insert into BinhLuan values (1,1,N'Phim quá hay','08/21/2021')
insert into BinhLuan values (2,1,N'Khi nào mới ra phim mới','08/21/2021')

select p.id_phim, p.ten_phim, l.ten_loai 
from Phim p INNER JOIN ChiTietLoai ctl 
	on p.id_phim = ctl.id_phim INNER JOIN Loai l
	on ctl.id_loai = l.id_loai

select *
from Phim p INNER JOIN ChiTietLoai ctl 
	on p.id_phim = ctl.id_phim 
	where ctl.id_loai =3

select * from Phim
where ten_phim like '%Thor%'

select p.ten_phim, nd.ten_nguoi_dung, nd.tai_khoan, bl.noi_dung,bl.ngay_binh_luan 
from Phim p INNER JOIN BinhLuan bl on p.id_phim= bl.id_phim INNER JOIN NguoiDung nd on bl.id_nguoi_dung = nd.id_nguoi_dung



select * from AdminPro
select * from BinhLuan
select * from ChiTietLoai
select * from DanhGia
select * from DienVien
select * from Loai
select * from NamPhatHanh
select * from NguoiDung
select * from Phim
select * from Phim_Dienvien
select * from QuocGia
select * from TrangThai

select * from Banner

