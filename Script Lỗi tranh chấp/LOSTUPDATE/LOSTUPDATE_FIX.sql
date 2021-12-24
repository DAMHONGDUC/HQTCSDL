﻿USE QL_DH_GH
GO

--DROP PROC Sp_NV_DuyetHopDong
--DROP PROC Sp_NV_LoaiBoHopDong	

CREATE 
--ALTER
PROC Sp_NV_DuyetHopDong
	@NGAYBATDAU DATE,
	@NGAYKETTHUC DATE,
	@MANV VARCHAR(15),
	@MAHD VARCHAR(15)	
	
AS
BEGIN TRAN
	BEGIN TRY
	IF NOT EXISTS (SELECT * 
				FROM HOPDONG 
				WHERE MAHD = @MAHD )
	BEGIN
		PRINT CAST(@MAHD AS VARCHAR(15)) + N' Không Tồn Tại'
		ROLLBACK TRAN
		RETURN 0
	END

	IF NOT EXISTS (SELECT * 
				FROM NHANVIEN
				WHERE MANV = @MANV )
	BEGIN
		PRINT CAST(@MANV AS VARCHAR(15)) + N' Không Tồn Tại'
		ROLLBACK TRAN
		RETURN 0
	END

	--kiểm tra xem hợp đồng đã được xử lí hay chưa
	DECLARE @DADUYET INT
	SET @DADUYET = (SELECT DADUYET FROM HOPDONG WITH (XLOCK) WHERE MAHD = @MAHD )
	IF (@DADUYET != 0) 
	BEGIN
		PRINT N'Hợp đồng đã được xử lí'
		ROLLBACK TRAN
		RETURN 0
	END

	--ĐỂ TEST
	WAITFOR DELAY '0:0:10'
	-------------

	--set tình trạng duyệt
	SET @DADUYET = 1

	-- duyệt hợp đồng
	UPDATE HOPDONG
	SET DADUYET = 1, NGAYBATDAU = @NGAYBATDAU, NGAYKETTHUC = @NGAYKETTHUC, MANV = @MANV
	WHERE MAHD = @MAHD 	
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 0	
	END CATCH
COMMIT TRAN
RETURN 1
GO

CREATE 
--ALTER
PROC Sp_NV_LoaiBoHopDong	
	@MANV VARCHAR(15),
	@MAHD VARCHAR(15)
AS
BEGIN TRAN
	BEGIN TRY
	IF NOT EXISTS (SELECT * 
				FROM HOPDONG 
				WHERE MAHD = @MAHD )
	BEGIN
		PRINT CAST(@MAHD AS VARCHAR(15)) + N' Không Tồn Tại'
		ROLLBACK TRAN
		RETURN 0
	END
	IF NOT EXISTS (SELECT * 
				FROM NHANVIEN
				WHERE MANV = @MANV )
	BEGIN
		PRINT CAST(@MANV AS VARCHAR(15)) + N' Không Tồn Tại'
		ROLLBACK TRAN
		RETURN 0
	END

	--kiểm tra xem hợp đồng đã được xử lí hay chưa
	DECLARE @DADUYET INT
	SET @DADUYET = (SELECT DADUYET FROM HOPDONG WHERE MAHD = @MAHD )
	IF (@DADUYET != 0) 
	BEGIN
		PRINT N'Hợp đồng đã được xử lí'
		ROLLBACK TRAN
		RETURN 0
	END

	--set tình trạng loại bỏ
	SET @DADUYET = 2

	--Không duyệt hợp đồng	
	UPDATE HOPDONG
	SET DADUYET = @DADUYET
	WHERE MAHD = @MAHD 	
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 0
	END CATCH
COMMIT TRAN
RETURN 1
GO

