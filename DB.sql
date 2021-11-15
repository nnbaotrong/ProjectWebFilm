
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

delete Loai
DBCC CHECKIDENT (Loai, RESEED, 0)
DBCC CHECKIDENT (Loai)
insert into loai values ('Phiêu Lưu'),(N'Hành Động'),(N'Nhập Vai')



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

delete TrangThai
DBCC CHECKIDENT (TrangThai, RESEED, 0)
DBCC CHECKIDENT (TrangThai)
insert into TrangThai values (N'Đã Phát Hành')
insert into TrangThai values (N'Chưa Phát Hành')

delete NguoiDung
DBCC CHECKIDENT (NguoiDung, RESEED, 0)
DBCC CHECKIDENT (NguoiDung)
insert into NguoiDung values (N'Nguyễn Ngọc Bảo Trọng',N'nnbaotrong',N'123','nnbaotrong@gmail.com')


delete AdminPro
DBCC CHECKIDENT (AdminPro, RESEED, 0)
DBCC CHECKIDENT (AdminPro)
insert into AdminPro values (N'admin',N'123')

delete Phim
DBCC CHECKIDENT (Phim, RESEED, 0)
DBCC CHECKIDENT (Phim)
insert into Phim values (N'Iron Man',N'Iron Man.jpg','https://www.youtube.com/embed/y_-1uiB2T9Y',N'Phim Hay vler','https://www.youtube.com/embed/8ugaeA-nMTc',1,1,1)
insert into Phim values (N'Iron Man 2',N'Iron Man 2.jpg','https://www.youtube.com/embed/y_-1uiB2T9Y',N'Phim Hay vler','https://www.youtube.com/embed/BoohRoVA9WQ',1,5,2)
insert into Phim values (N'Iron Man 3',N'Iron Man 3.jpg','https://www.youtube.com/embed/y_-1uiB2T9Y',N'Phim Hay vler','https://www.youtube.com/embed/Ke1Y3P9D0Bc',1,4,1)
insert into Phim values (N'Captain America 2',N'Captain America 2.jpg','https://www.youtube.com/embed/y_-1uiB2T9Y',N'Phim Hay vler','https://www.youtube.com/embed/7SlILk2WMTI',1,2,1)

delete ChiTietLoai
insert into ChiTietLoai values (1,1)
insert into ChiTietLoai values (1,3)
insert into ChiTietLoai values (1,2)
insert into ChiTietLoai values (2,2)
insert into ChiTietLoai values (2,3)
insert into ChiTietLoai values (3,3)
insert into ChiTietLoai values (3,1)

delete Phim_Dienvien
insert into Phim_Dienvien values (1,1)
insert into Phim_Dienvien values (1,2)
insert into Phim_Dienvien values (2,1)
insert into Phim_Dienvien values (2,3)
insert into Phim_Dienvien values (3,3)


select p.id_phim, p.ten_phim, l.ten_loai 
from Phim p INNER JOIN ChiTietLoai ctl 
	on p.id_phim = ctl.id_phim INNER JOIN Loai l
	on ctl.id_loai = l.id_loai






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



