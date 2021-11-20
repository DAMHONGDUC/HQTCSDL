﻿USE master

IF DB_ID('QL_DH_GH') IS NOT NULL
	DROP DATABASE QL_DH_GH
GO

CREATE DATABASE QL_DH_GH

GO 

USE QL_DH_GH

GO 

-- Đăng kí thông tin 
CREATE TABLE DOITAC
(
	MAACC VARCHAR(15),
	MADT VARCHAR(15),
	TENDT NVARCHAR(50) NOT NULL,
	NGUOIDAIDIEN NVARCHAR(50) NOT NULL,
	THANHPHO NVARCHAR(50) NOT NULL,
	QUAN NVARCHAR(50) NOT NULL,
	SOCHINHANH INT NOT NULL,
	SLDONHANG_MOINGAY INT NOT NULL,
	LOAIHANG VARCHAR(15) NOT NULL,
	DIACHI  NVARCHAR(50) NOT NULL,
	SDT VARCHAR(15) NOT NULL,
	EMAIL VARCHAR(50) NOT NULL,
	MASOTHUE VARCHAR(15) NOT NULL

	CONSTRAINT PK_DOITAC
	PRIMARY KEY (MADT)
)
GO

-- Lập hợp đồng
CREATE TABLE HOPDONG
(
	MAHD VARCHAR(15),
	MADT VARCHAR(15),
	SLCHINHANH INT NOT NULL,
	PHIKICHHOAT INT NOT NULL,
	PTHOAHONG INT NOT NULL,
	NGAYLAP DATE NOT NULL,
	NGAYKETTHUC DATE NOT NULL,
	DADUYET INT NOT NULL,

	CONSTRAINT PK_HOPDONG
	PRIMARY KEY (MAHD)
)
GO

-- Chi tiết hợp đồng
CREATE TABLE CT_HOPDONG
(
	MAHD VARCHAR(15),
	MADT VARCHAR(15),
	MACHINHANH VARCHAR(15)
	
	CONSTRAINT PK_CT_HOPDONG
	PRIMARY KEY (MAHD,MACHINHANH)
)
GO

-- Chi Nhánh
CREATE TABLE CHINHANH
(	
	MACHINHANH VARCHAR(15),
	TENCHINHANH NVARCHAR(50),
	MADT VARCHAR(15),
	DIACHI NVARCHAR(50) NOT NULL,
	
	CONSTRAINT PK_CHINHANH
	PRIMARY KEY (MACHINHANH,MADT)
)
GO

-- Sản Phẩm
CREATE TABLE SANPHAM
(
	MASP VARCHAR(15),
	MADT VARCHAR(15),
	TENSP NVARCHAR(50) NOT NULL,
	CHINHANH VARCHAR(15),
	SOLUONG INT NOT NULL,
	GIABAN DECIMAL(19,4) NOT NULL

	CONSTRAINT PK_SANPHAM
	PRIMARY KEY (MASP,MADT)
)
GO

-- Khách Hàng
CREATE TABLE KHACHHANG
(
	MAKH VARCHAR(15),
	MAACC VARCHAR(15),	
	HOTEN NVARCHAR(50) NOT NULL,
	SDT VARCHAR(15) NOT NULL,
	DIACHI NVARCHAR(50) NOT NULL,
	EMAIL VARCHAR(50) NOT NULL,
	STK VARCHAR(20)

	CONSTRAINT PK_KHACHHANG
	PRIMARY KEY (MAKH)
)
GO

-- Đơn Hàng
CREATE TABLE DONHANG
(
	MADH VARCHAR(15),
	MADT VARCHAR(15),	
	MAKH VARCHAR(15),
	SOLUONGSP INT NOT NULL,
	HINHTHUCTHANHTOAN INT NOT NULL,
	DIACHIGH NVARCHAR(50) NOT NULL,
	NGAYLAP DATETIME NOT NULL,
	TONGPHISP DECIMAL(19,4) NOT NULL,
	PHIVANCHUYEN DECIMAL(19,4) NOT NULL,
	TONGPHI DECIMAL(19,4) NOT NULL,
	TINHTRANG INT NOT NULL

	CONSTRAINT PK_DONHANG
	PRIMARY KEY (MADH)
) 
GO

-- CT Đơn Hàng
CREATE TABLE CT_DONHANG
(
	MADT VARCHAR(15),
	MADH VARCHAR(15),
	MASP VARCHAR(15),
	SOLUONG INT NOT NULL,
	THANHTIEN DECIMAL(19,4) NOT NULL

	CONSTRAINT PK_CT_DONHANG
	PRIMARY KEY (MADH, MASP)
)
GO

-- Tài Xế
CREATE TABLE TAIXE
(
	MATX VARCHAR(15),
	MAACC VARCHAR(15),
	HOTEN NVARCHAR(50) NOT NULL,
	CMND VARCHAR(20) NOT NULL,
	SDT VARCHAR(15) NOT NULL,
	DIACHI NVARCHAR(50) NOT NULL,
	BIENSOXE VARCHAR(20) NOT NULL,
	KHUVUCHOATDONG NVARCHAR(50) NOT NULL,
	EMAIL VARCHAR(50) NOT NULL,
	STK VARCHAR(20) NOT NULL,
	THECHAN INT NOT NULL

	CONSTRAINT PK_TAIXE
	PRIMARY KEY (MATX)
) 
GO

-- Tiếp nhận và xử lí đơn hàng
CREATE TABLE XULI_DONHANG
(	
	MADH VARCHAR(15),
	MATX VARCHAR(15),
	NGAYTXNHAN DATE,
	NGAYKHNHAN DATE

	CONSTRAINT PK_XULI_DONHANG
	PRIMARY KEY (MADH)
)
GO

-- thêm mới 
CREATE TABLE NHANVIEN
(
	MANV VARCHAR(15),
	MAACC VARCHAR(15),	
	TENNV NVARCHAR(50) NOT NULL,
	SDT VARCHAR(15) NOT NULL,
	DIACHI NVARCHAR(50) NOT NULL,
	EMAIL VARCHAR(50) NOT NULL

	CONSTRAINT PK_NHANVIEN
	PRIMARY KEY (MANV)
)
GO

-- Accounts
CREATE TABLE ACCOUNT
(
	MAACC VARCHAR(15),
	TENDANGNHAP VARCHAR(50) NOT NULL,
	MATKHAU VARCHAR(50) NOT NULL,
	LOAIACC INT NOT NULL

	CONSTRAINT PK_ACCOUNT
	PRIMARY KEY (MAACC)
)
GO

-- thêm mới
ALTER TABLE NHANVIEN
ADD
	CONSTRAINT FK_NV_ACC
	FOREIGN KEY (MAACC)
	REFERENCES ACCOUNT(MAACC)
GO

-- thêm mới
ALTER TABLE KHACHHANG
ADD
	CONSTRAINT FK_KH_ACC
	FOREIGN KEY (MAACC)
	REFERENCES ACCOUNT(MAACC)
GO

-- thêm mới
ALTER TABLE TAIXE
ADD
	CONSTRAINT FK_TX_ACC
	FOREIGN KEY (MAACC)
	REFERENCES ACCOUNT(MAACC)
GO

-- thêm mới
ALTER TABLE DOITAC
ADD
	CONSTRAINT FK_DT_ACC
	FOREIGN KEY (MAACC)
	REFERENCES ACCOUNT(MAACC)
GO

ALTER TABLE HOPDONG
ADD
	CONSTRAINT FK_HD_DOITAC
	FOREIGN KEY (MADT)
	REFERENCES DOITAC(MADT)
GO

ALTER TABLE CT_HOPDONG
ADD
	CONSTRAINT FK_CTHD_HD
	FOREIGN KEY (MAHD)
	REFERENCES HOPDONG(MAHD),

	CONSTRAINT FK_CTHD_CN
	FOREIGN KEY (MACHINHANH, MADT)
	REFERENCES CHINHANH
GO

ALTER TABLE SANPHAM
ADD
	CONSTRAINT FK_SP_CN
	FOREIGN KEY (CHINHANH,MADT)
	REFERENCES CHINHANH
GO

ALTER TABLE DONHANG
ADD
	CONSTRAINT FK_DH_KH
	FOREIGN KEY (MAKH)
	REFERENCES KHACHHANG(MAKH),

	CONSTRAINT FK_DH_DT
	FOREIGN KEY (MADT)
	REFERENCES DOITAC(MADT)
GO

ALTER TABLE  CT_DONHANG
ADD
	CONSTRAINT FK_CTDH_SP
	FOREIGN KEY (MASP,MADT)
	REFERENCES SANPHAM,

	CONSTRAINT FK_CTDH_DH
	FOREIGN KEY (MADH)
	REFERENCES DONHANG(MADH)
GO

ALTER TABLE XULI_DONHANG
ADD
	CONSTRAINT FK_XLDH_DH
	FOREIGN KEY (MADH)
	REFERENCES DONHANG(MADH),

	CONSTRAINT FK_CTDH_TX
	FOREIGN KEY (MATX)
	REFERENCES TAIXE(MATX)
GO

--	ADMIN 
EXEC sp_addlogin 'QL_DH_GH_ADMIN', '12345', 'QL_DH_GH';  
GO

EXEC sp_addsrvrolemember 'QL_DH_GH_ADMIN', 'sysadmin';  
GO

-- VODANH
EXEC sp_addlogin 'QL_DH_GH_VODANH', '12345', 'QL_DH_GH';  
GO

CREATE USER VODANH FOR LOGIN QL_DH_GH_VODANH
GO

GRANT SELECT, INSERT ON dbo.ACCOUNT TO VODANH 
GO

-- DOITAC
EXEC sp_addlogin 'QL_DH_GH_DOITAC', '12345', 'QL_DH_GH';  
GO

CREATE USER DOITAC FOR LOGIN QL_DH_GH_DOITAC
GO

GRANT SELECT, INSERT, UPDATE ON DOITAC TO DOITAC
GRANT SELECT, INSERT ON HOPDONG TO DOITAC
GRANT SELECT, INSERT ON CT_HOPDONG TO DOITAC
GRANT SELECT, INSERT, UPDATE, DELETE ON SANPHAM TO DOITAC
GRANT SELECT ON DONHANG TO DOITAC
GRANT SELECT ON CT_DONHANG TO DOITAC
GRANT UPDATE ON DONHANG(TINHTRANG) TO DOITAC
GRANT SELECT ON XULI_DONHANG TO DOITAC
GRANT SELECT ON TAIXE(HOTEN,SDT) TO DOITAC
GRANT SELECT, INSERT, UPDATE,DELETE ON CHINHANH TO DOITAC
GRANT SELECT ON KHACHHANG(HOTEN,SDT) TO DOITAC
GO

-- KHACHHANG
EXEC sp_addlogin 'QL_DH_GH_KHACHHANG', '12345', 'QL_DH_GH';  
GO

CREATE USER KHACHHANG FOR LOGIN QL_DH_GH_KHACHHANG
GO

GRANT SELECT, INSERT, UPDATE ON KHACHHANG TO KHACHHANG
GRANT SELECT ON DOITAC(TENDT,DIACHI,SOCHINHANH,LOAIHANG,SDT) TO KHACHHANG
GRANT SELECT ON SANPHAM TO KHACHHANG
GRANT SELECT, INSERT ON DONHANG TO KHACHHANG
GRANT SELECT, INSERT ON CT_DONHANG TO KHACHHANG
GRANT SELECT ON XULI_DONHANG TO KHACHHANG
GRANT SELECT ON CHINHANH TO KHACHHANG
GRANT SELECT ON TAIXE(HOTEN,SDT) TO KHACHHANG
GO

-- TAIXE
EXEC sp_addlogin 'QL_DH_GH_TAIXE', '12345', 'QL_DH_GH';  
GO

CREATE USER TAIXE FOR LOGIN QL_DH_GH_TAIXE
GO

GRANT SELECT, INSERT ON TAIXE TO TAIXE
GRANT SELECT ON DONHANG TO TAIXE
GRANT SELECT ON CT_DONHANG TO TAIXE
GRANT SELECT ON KHACHHANG(MAKH,HOTEN,SDT) TO TAIXE
GRANT SELECT,UPDATE ON XULI_DONHANG TO TAIXE
GRANT UPDATE ON XULI_DONHANG(MATX,NGAYTXNHAN,NGAYKHNHAN) TO TAIXE
GO

-- NHANVIEN
EXEC sp_addlogin 'QL_DH_GH_NHANVIEN', '12345', 'QL_DH_GH';  
GO

CREATE USER NHANVIEN FOR LOGIN QL_DH_GH_NHANVIEN
GO

GRANT SELECT, INSERT ON NHANVIEN TO NHANVIEN 
GRANT SELECT ON DOITAC TO NHANVIEN 
GRANT SELECT ON HOPDONG TO NHANVIEN 
GRANT UPDATE ON HOPDONG(DADUYET) TO NHANVIEN 
GO

-- TẠO ROLE
EXEC SP_ADDROLE 'ROLE_USERS'

-- ADD THÊM USER VÀO ROLE NÀY
EXEC sp_addrolemember 'ROLE_USERS', 'DOITAC'
EXEC sp_addrolemember 'ROLE_USERS', 'KHACHHANG'
EXEC sp_addrolemember 'ROLE_USERS', 'TAIXE'
EXEC sp_addrolemember 'ROLE_USERS', 'NHANVIEN'

-- CẤP QUYỀN CHO ROLE
GRANT SELECT, UPDATE ON ACCOUNT(MATKHAU) TO ROLE_USERS

--INPUT DATA
INSERT INTO ACCOUNT(MAACC, TENDANGNHAP, MATKHAU, LOAIACC)
VALUES
	('DT001','devnguyen','12345',0),
	('DT111','rambo','12345',0),
	('NV001','dukdam','12345',3),
	('NV111','fuocwuy','12345',3),
	('KH001','him','12345',1),
	('KH111','her','12345',1),
	('TX001','lisa','12345',2),
	('TX111','jennie','12345',2),
	('AD001','rose','12345',4),
	('AD111','jisoo','12345',4)	
GO

INSERT INTO DOITAC(MADT,MAACC, TENDT, NGUOIDAIDIEN, THANHPHO, QUAN, SOCHINHANH, SLDONHANG_MOINGAY,
LOAIHANG, DIACHI, SDT, EMAIL, MASOTHUE)
VALUES
	('DT001', 'DT001', N'MixiFood', N'Độ Mixi', N'Hà Nội', N'Yên Lãng', 3, 100, 'food', N'123 Yên Lãng, Tp. Hà Nội', 
	'0123456789', 'mixifood@gmail.com', 'MST001'),
	('DT111', 'DT111', N'BlackPink', N'YG', N'Hồ Chí Minh', N'1', 100, 500, 'music', N'1 F1, Q1, Tp. HCM', 
	'0147852369', 'blackpink@gmail.com', 'MST111')
GO

INSERT INTO HOPDONG(MAHD, MADT, SLCHINHANH, PHIKICHHOAT, PTHOAHONG, NGAYLAP, NGAYKETTHUC, DADUYET)
VALUES
	('HD001', 'DT001', 2, 1, 10, '12/22/2020','12/22/2022',1),
	('HD111', 'DT111', 15, 0, 20, '1/1/2020','1/1/2025',0)
GO

INSERT INTO CHINHANH(MACHINHANH,MADT,TENCHINHANH,DIACHI)
VALUES
	('CN001','DT001',N'Hai Bà Trưng',N'Q5, TP HCM'),
	('CN111','DT111',N'Nguyễn Văn Trỗi',N'Thanh Xuân, tp Hà Nội')
GO

INSERT INTO CT_HOPDONG(MAHD,MADT,MACHINHANH)
VALUES
	('HD001','DT001','CN001'),
	('HD111','DT111','CN111')
GO

INSERT INTO SANPHAM(MASP, MADT, TENSP, CHINHANH, SOLUONG, GIABAN)
VALUES
	('SP001','DT001','Kho Ga', 'CN001',50,40000),
	('SP111','DT111','CD','CN111',1000,15000)
GO

INSERT INTO KHACHHANG(MAKH, MAACC, HOTEN, SDT, DIACHI, EMAIL, STK)
VALUES
	('KH001','KH001',N'Sơn Tùng MTP', '0157482369','Q7, SG','mtp@mtp.com','VN1234'),
	('KH111','KH111',N'G Dragon','0235698741','Hàng Mã, Hà Nội','gd@gd.com',null)
GO

INSERT INTO DONHANG(MADH,MADT,MAKH,SOLUONGSP,HINHTHUCTHANHTOAN, DIACHIGH, NGAYLAP, TONGPHISP, PHIVANCHUYEN, TONGPHI, TINHTRANG)
VALUES
	('DH001','DT001','KH001',5,2,N'Trần Phú, F3, Tp.Đà Lạt','11/19/2021',200000,40000,240000,1),
	('DH111','DT111','KH111',3,0,N'Nhà Chung, F3, Tp. Đà Lạt','11/19/2021',70000,40000,110000,0)
GO

INSERT INTO CT_DONHANG(MADT, MADH, MASP, SOLUONG, THANHTIEN)
VALUES
	('DT001','DH001','SP001',5,200000),
	('DT001','DH111','SP001',1,40000),
	('DT111','DH111','SP111',2,30000)
GO

INSERT INTO TAIXE(MATX,MAACC,HOTEN,CMND,SDT,DIACHI,BIENSOXE,KHUVUCHOATDONG,EMAIL,STK,THECHAN)
VALUES
	('TX001','TX001',N'16 Typh','251211111','0125478369',N'Hải Phòng City','16F1 - 11638', N'Hải Phòng City','typh@gmail.com','VN1478',0),
	('TX111','TX111',N'Lil Wuyn','251222222','0245187369', N'Đà Lạt City','49B1 - 02868', N'F3, Tp. Đà Lạt','wuyn@gmail.com','VN1597',1)
GO

INSERT INTO XULI_DONHANG(MADH,MATX,NGAYTXNHAN, NGAYKHNHAN)
VALUES
	('DH111','TX111','11/19/2021',NULL)
GO

INSERT INTO NHANVIEN(MANV, MAACC, TENNV, SDT, DIACHI, EMAIL)
VALUES 
	('NV001','NV001',N'TRẦN THIỆN THANH BẢO','0124853679',N'Tp.HCM','bray@gmail.com'),
	('NV111','NV111',N'VŨ ĐỨC THIỆN','0123587496','Tp. Hà Nội', 'rhymastic@gmail.com')
GO

