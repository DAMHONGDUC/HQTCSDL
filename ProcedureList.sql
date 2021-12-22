USE QL_DH_GH
GO

--DROP PROC Sp_DangNhap
--DROP PROC Sp_NV_DuyetHopDong
--DROP PROC Sp_NV_LoaiBoHopDong
--DROP PROC Sp_NV_DoiMK
--DROP PROC Sp_NV_DoiThongTinTK

-- Xử lí đăng nhập tài khoản
CREATE PROC Sp_DangNhap
	@TENDANGNHAP VARCHAR(50),
	@MATKHAU VARCHAR(50),
	@MAACC VARCHAR(15) OUTPUT,
	@LOAIACC INT OUTPUT
AS
BEGIN
	SET @MAACC = 'NULL'
	IF NOT EXISTS (SELECT MAACC
				FROM ACCOUNT 
				WHERE TENDANGNHAP = @TENDANGNHAP 
				AND MATKHAU = @MATKHAU)
	BEGIN
		PRINT N'Sai tên đăng nhập hoặc mật khẩu'
		RETURN 0
	END
	
	-- lấy mã acc
	SET @MAACC = (SELECT MAACC
				FROM ACCOUNT
				WHERE TENDANGNHAP = @TENDANGNHAP
				AND MATKHAU = @MATKHAU)

	-- lấy loại acc
	SET @LOAIACC = (SELECT LOAIACC
				FROM ACCOUNT
				WHERE TENDANGNHAP = @TENDANGNHAP
				AND MATKHAU = @MATKHAU)

	-- xử lí đăng nhập
	if (@MAACC != 'NULL')
	BEGIN
		PRINT N'Đăng nhập thành công'
		RETURN 1
	END
	ELSE RETURN 0	
END
GO

--------------------PROCEDURE-PHẦN CỦA ĐỨC

-- Nhân viên duyệt hợp đồng
CREATE 
PROC Sp_NV_DuyetHopDong
	@NGAYBATDAU DATE,
	@NGAYKETTHUC DATE,
	@MANV VARCHAR(15),
	@MAHD VARCHAR(15)	
AS
BEGIN
	--kiểm tra mã hợp đồng có tồn tại hay không
	IF NOT EXISTS (SELECT MAHD 
				FROM HOPDONG 
				WHERE MAHD = @MAHD )
	BEGIN
		PRINT CAST(@MAHD AS VARCHAR(15)) + N' Không Tồn Tại'
		RETURN 0
	END

	--kiểm tra mã nhân viên có tồn tại hay không
	IF NOT EXISTS (SELECT MANV
				FROM NHANVIEN
				WHERE MANV = @MANV )
	BEGIN
		PRINT CAST(@MANV AS VARCHAR(15)) + N' Không Tồn Tại'
		RETURN 0
	END

	-- duyệt hợp đồng
	UPDATE HOPDONG
	SET DADUYET = 1, NGAYBATDAU = @NGAYBATDAU, NGAYKETTHUC = @NGAYKETTHUC, MANV = @MANV
	WHERE MAHD = @MAHD 	
	RETURN 1
END
GO

-- Nhân viên loại bỏ hợp đồng (không duyệt hợp đồng)
CREATE 

PROC Sp_NV_LoaiBoHopDong	
	@MANV VARCHAR(15),
	@MAHD VARCHAR(15)
AS
BEGIN
	--kiểm tra mã hợp đồng có tồn tại hay không
	IF NOT EXISTS (SELECT * 
				FROM HOPDONG 
				WHERE MAHD = @MAHD )
	BEGIN
		PRINT CAST(@MAHD AS VARCHAR(15)) + N' Không Tồn Tại'
		RETURN 0
	END

	--kiểm tra mã nhân viên có tồn tại hay không
	IF NOT EXISTS (SELECT * 
				FROM NHANVIEN
				WHERE MANV = @MANV )
	BEGIN
		PRINT CAST(@MANV AS VARCHAR(15)) + N' Không Tồn Tại'
		RETURN 0
	END

	-- loại bỏ hợp đồng
	UPDATE HOPDONG
	SET DADUYET = 2, MANV = @MANV
	WHERE MAHD = @MAHD 	
	RETURN 1
END
GO

--Đổi mật khẩu tài khoản nhân viên
CREATE PROC Sp_NV_DoiMK
	@MAACC VARCHAR(15),
	@MATKHAU VARCHAR(50)
AS
BEGIN
	--kiểm tra mã nhân viên có tồn tại hay không
	IF NOT EXISTS (SELECT * 
				FROM ACCOUNT
				WHERE MAACC = @MAACC)
	BEGIN
		PRINT CAST(@MAACC AS VARCHAR(15)) + N' Không Tồn Tại'
		RETURN 0
	END	

	-- xử lí Update mật khẩu
	UPDATE ACCOUNT
	SET MATKHAU = @MATKHAU 
	WHERE MAACC = @MAACC	
	RETURN 1
END
GO

--Đổi thông tin tài khoản nhân viên
CREATE PROC Sp_NV_DoiThongTinTK
	@MANV VARCHAR(15),
	@TENNV NVARCHAR(50),
	@SDT VARCHAR(15) ,
	@DIACHI NVARCHAR(50),
	@EMAIL VARCHAR(50) 
AS
BEGIN
	--kiểm tra mã nhân viên có tồn tại hay không
	IF NOT EXISTS (SELECT * 
				FROM NHANVIEN
				WHERE MANV = @MANV)
	BEGIN
		PRINT CAST(@MANV AS VARCHAR(15)) + N' Không Tồn Tại'
		RETURN 0
	END	

	-- xử lí Update
	UPDATE NHANVIEN
	SET TENNV = @TENNV, SDT = @SDT, DIACHI = @DIACHI, EMAIL = @EMAIL
	WHERE MANV = @MANV	
	RETURN 1
END
GO

----PROCEDURE CỦA MINH
--PROCEDURE ĐỐI TÁC THÊM CHI NHÁNH
CREATE PROCEDURE sp_DT_ThemChiNhanh @madt VARCHAR(15), @machinhanh VARCHAR(15), @diachi NVARCHAR(50), @ten NVARCHAR(50), @res int output
AS
	--Kiểm tra địa chỉ có trùng hay không
	IF(EXISTS(SELECT * FROM CHINHANH WHERE MADT = @MADT AND DIACHI = @diachi))
			RETURN @res = -1

	-- Kiểm tra mã chi nhánh có trùng hay không
	IF(EXISTS(SELECT * FROM CHINHANH WHERE MACHINHANH = @machinhanh))
			RETURN @res = -1
	
	INSERT INTO CHINHANH(MACHINHANH, MADT, TENCHINHANH, DIACHI)
	VALUES
		(@machinhanh, @madt, @ten, @diachi)
	return @res = 1
GO

























