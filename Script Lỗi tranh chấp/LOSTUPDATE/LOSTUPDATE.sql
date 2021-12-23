﻿USE QL_DH_GH
GO

--DROP PROC Sp_NV_DuyetHopDong1
--DROP PROC Sp_NV_DuyetHopDong2

CREATE 
--ALTER
PROC Sp_NV_DuyetHopDong1
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

	-- duyệt hợp đồng
	UPDATE HOPDONG
	SET DADUYET = 1
	WHERE MAHD = @MAHD 	
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 0	
	END CATCH
COMMIT TRAN

--ĐỂ TEST
	WAITFOR DELAY '0:0:10'
	-------------

RETURN 1
GO

CREATE 
--ALTER
PROC Sp_NV_DuyetHopDong2
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

	--Không duyệt hợp đồng	
	UPDATE HOPDONG
	SET DADUYET = 2
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

