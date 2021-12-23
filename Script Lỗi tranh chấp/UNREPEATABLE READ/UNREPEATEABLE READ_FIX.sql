USE QL_DH_GH
GO

--DROP PROC Sp_NV_DoiMK
--DROP PROC Sp_NV_LayTongTinTK

--Đổi mật khẩu tài khoản nhân viên
CREATE PROC Sp_NV_DoiMK
	@MAACC VARCHAR(15),
	@MATKHAU VARCHAR(50)
AS
BEGIN TRAN
	BEGIN TRY
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
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 0
	END CATCH
COMMIT TRAN
RETURN 1
GO

--Lấy thông tin tài khoản nhân viên
CREATE PROC Sp_NV_LayTongTinTK	@TENDANGNHAP VARCHAR(15),
	@MATKHAU VARCHAR(50)ASSET TRAN ISOLATION LEVEL REPEATABLE READBEGIN TRAN	BEGIN TRY			DECLARE @MAACC VARCHAR(15)	SET @MAACC = 'NULL'	-- xử lí lấy thông tin mã acc
	SET @MAACC = (SELECT A.MAACC            
                FROM ACCOUNT A, NHANVIEN NV 
                WHERE A.TENDANGNHAP = @TENDANGNHAP 
                AND A.MATKHAU =   @MATKHAU 
                AND A.MAACC = NV.MAACC)	--ĐỂ TEST
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
                AND A.MAACC = NV.MAACC	END TRY	BEGIN CATCH		PRINT N'LỖI HỆ THỐNG'		ROLLBACK TRAN	END CATCHCOMMIT TRANGO