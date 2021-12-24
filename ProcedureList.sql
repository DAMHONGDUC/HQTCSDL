USE QL_DH_GH
GO

--DROP PROC Sp_DangNhap
--DROP PROC Sp_NV_DuyetHopDong
--DROP PROC Sp_NV_LoaiBoHopDong
--DROP PROC Sp_NV_DoiMK
--DROP PROC Sp_NV_DoiThongTinTK
<<<<<<< Updated upstream
=======
--DROP PROC DT_UPDATE_GiASP
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
	--kiểm tra mã nhân viên có tồn tại hay không
=======
	--kiểm tra mã tài khoản có tồn tại hay không
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
=======
--Lấy thông tin tài khoản nhân viên
CREATE PROC Sp_NV_LayTongTinTK
	@TENDANGNHAP VARCHAR(15),
	@MATKHAU VARCHAR(50)
AS
BEGIN
	DECLARE @MAACC VARCHAR(15)
	SET @MAACC = 'NULL'

	-- xử lí lấy thông tin mã acc
	SET @MAACC = (SELECT A.MAACC            
                FROM ACCOUNT A, NHANVIEN NV 
                WHERE A.TENDANGNHAP = @TENDANGNHAP 
                AND A.MATKHAU =   @MATKHAU 
                AND A.MAACC = NV.MAACC)

	--ĐỂ TEST
	WAITFOR DELAY '0:0:5'

	--kiểm tra tài khoản có tồn tại hay không
	IF (@MAACC = 'NULL')
	BEGIN
		PRINT N'Tài Khoản Không Tồn Tại'
		ROLLBACK TRAN
		RETURN 0
	END	

	-- xử lí lấy thông tin
	SELECT A.TENDANGNHAP, A.MATKHAU, NV.TENNV, NV.DIACHI, NV.SDT, NV.EMAIL, A.MAACC            
                FROM ACCOUNT A, NHANVIEN NV 
                WHERE A.TENDANGNHAP = @TENDANGNHAP 
                AND A.MATKHAU =   @MATKHAU 
                AND A.MAACC = NV.MAACC
END
GO
>>>>>>> Stashed changes

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

--------------------PROCEDURE-PHẦN CỦA Huy
--Khách hàng mua sản phẩm
CREATE 
--ALTER
PROC Sp_KH_MUASP
	@MASP VARCHAR(15),
	@SOLUONG INT
AS
	DECLARE @SOLUONGTON INT = (SELECT SOLUONG
							FROM SANPHAM 
							WHERE MASP = @MASP)
	IF (@SOLUONGTON >= @SOLUONG)
	BEGIN
		SET @SOLUONGTON = @SOLUONGTON - @SOLUONG
	END
		ELSE
	BEGIN
		PRINT N'SỐ LƯỢNG SẢN PHẨM CÒN LẠI KHÔNG ĐỦ'
		RETURN 0;
	END
	
	BEGIN
		UPDATE SANPHAM
		SET SOLUONG = @SOLUONGTON
		WHERE MASP = @MASP
		RETURN 1;
	END 

GO

<<<<<<< Updated upstream
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
=======
--DROP PROC DT_UPDATE_GiASP

-- Đối tác cập nhật giá sản phẩm
CREATE 
PROC DT_UPDATE_GiASP
	@MASP VARCHAR(15),
	@MADT VARCHAR(15),
	@GIAMOI DECIMAL(19,4)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS(SELECT *
					FROM SANPHAM
					WHERE MASP = @MASP AND MADT = @MADT)
		BEGIN
			PRINT N'SẢN PHẨM KHÔNG TỒN TẠI'
			ROLLBACK TRAN
			RETURN 1
		END
		

		UPDATE SANPHAM
		SET GIABAN = @GIAMOI
		WHERE MASP = @MASP AND MADT= @MADT 

		--ĐỂ TEST
		WAITFOR DELAY '0:0:20'

		IF @GIAMOI = 0
		BEGIN
			ROLLBACK TRAN 
			RETURN 1
		END
		-----
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1
	END CATCH
COMMIT TRAN
RETURN 0
GO

--DROP PROC Sp_KH_XEMSP

-- lấy thông tin sản phẩm
CREATE 
PROC Sp_KH_XEMSP
	@MADT VARCHAR(15)
AS
SET TRAN ISOLATION LEVEL READ UNCOMMITTED
BEGIN TRAN
	BEGIN TRY
		SELECT  SP.TENSP, SP.SOLUONG, SP.GIABAN, CN.DIACHI, SP.MASP 
                FROM SANPHAM SP, CHINHANH CN
                WHERE SP.CHINHANH = CN.MACHINHANH
                AND SP.MADT = CN.MADT
                AND SP.MADT = @MADT
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

--Khách hàng mua sản phẩm
CREATE 
--ALTER
PROC Sp_KH_MUASP
	@MASP VARCHAR(15),
	@SOLUONG INT
AS
BEGIN TRAN

	DECLARE @SOLUONGTON INT = (SELECT SOLUONG
							FROM SANPHAM WITH(HOLDLOCK)
							WHERE MASP = @MASP )
	WAITFOR DELAY '0:0:10'
	IF (@SOLUONGTON >= @SOLUONG)
	BEGIN
		SET @SOLUONGTON = @SOLUONGTON - @SOLUONG
	END
		ELSE
	BEGIN
		PRINT N'SỐ LƯỢNG SẢN PHẨM CÒN LẠI KHÔNG ĐỦ'
		ROLLBACK TRAN
		RETURN
	END
	BEGIN TRY
		UPDATE SANPHAM WITH(XLOCK)
		SET SOLUONG = @SOLUONGTON
		WHERE MASP = @MASP
	END TRY
	BEGIN CATCH 
		DECLARE @ErrorMsg VARCHAR(2000)
		SELECT @ErrorMsg = N'Lỗi: ' + ERROR_MESSAGE()
		RAISERROR(@ErrorMsg, 16,1)
		ROLLBACK TRAN
		RETURN
	END CATCH
COMMIT TRAN
GO


----PROCEDURE CỦA MINH
--PROCEDURE ĐỐI TÁC THÊM CHI NHÁNH
CREATE PROCEDURE sp_DT_ThemChiNhanh @madt VARCHAR(15), @machinhanh VARCHAR(15), @diachi NVARCHAR(50), @ten NVARCHAR(50)
AS
	--Kiểm tra địa chỉ có trùng hay không
	IF(EXISTS(SELECT * FROM CHINHANH WHERE MADT = @MADT AND DIACHI = @diachi))
			RETURN  -1

	-- Kiểm tra mã chi nhánh có trùng hay không
	IF(EXISTS(SELECT * FROM CHINHANH WHERE MACHINHANH = @machinhanh))
			RETURN -1
>>>>>>> Stashed changes
	
	INSERT INTO CHINHANH(MACHINHANH, MADT, TENCHINHANH, DIACHI)
	VALUES
		(@machinhanh, @madt, @ten, @diachi)
<<<<<<< Updated upstream
	return @res = 1
=======
	return 1
>>>>>>> Stashed changes
GO