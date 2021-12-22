﻿USE QL_DH_GH
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

GRANT EXECUTE ON OBJECT::Sp_DangNhap TO VODANH

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

GRANT EXECUTE ON OBJECT::sp_DT_ThemChiNhanh TO DOITAC
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
GRANT UPDATE ON HOPDONG(DADUYET,NGAYBATDAU,NGAYKETTHUC) TO NHANVIEN 
GRANT SELECT ON ACCOUNT(MAACC, TENDANGNHAP, MATKHAU) TO NHANVIEN
GRANT UPDATE ON ACCOUNT(MATKHAU) TO NHANVIEN
GRANT SELECT ON NHANVIEN(MAACC) TO NHANVIEN
GRANT SELECT, UPDATE ON NHANVIEN(TENNV, SDT, DIACHI, EMAIL) TO NHANVIEN
GO

GRANT EXECUTE ON OBJECT::Sp_NV_DuyetHopDong TO NHANVIEN

-- TẠO ROLE
EXEC SP_ADDROLE 'ROLE_USERS'

-- ADD THÊM USER VÀO ROLE NÀY
EXEC sp_addrolemember 'ROLE_USERS', 'DOITAC'
EXEC sp_addrolemember 'ROLE_USERS', 'KHACHHANG'
EXEC sp_addrolemember 'ROLE_USERS', 'TAIXE'
EXEC sp_addrolemember 'ROLE_USERS', 'NHANVIEN'

-- CẤP QUYỀN CHO ROLE
GRANT SELECT, UPDATE ON ACCOUNT(MATKHAU) TO ROLE_USERS
