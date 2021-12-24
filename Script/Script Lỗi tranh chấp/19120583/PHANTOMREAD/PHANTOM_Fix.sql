use QL_DH_GH

--PROCEDURE ĐỐI TÁC THÊM CHI NHÁNH
CREATE PROCEDURE sp_DT_ThemChiNhanh @madt VARCHAR(15), @machinhanh VARCHAR(15), @diachi NVARCHAR(50), @ten NVARCHAR(50)
AS

BEGIN TRAN
	BEGIN TRY
	--Kiểm tra địa chỉ có trùng hay không
	IF(EXISTS(SELECT * FROM CHINHANH WHERE MADT = @MADT AND DIACHI = @diachi))
			begin
			rollback tran
			RETURN  -1
			end

	-- Kiểm tra mã chi nhánh có trùng hay không
	IF(EXISTS(SELECT * FROM CHINHANH WHERE MACHINHANH = @machinhanh))
			begin
			rollback tran
			RETURN  -1
			end
	
	INSERT INTO CHINHANH(MACHINHANH, MADT, TENCHINHANH, DIACHI)
	VALUES
		(@machinhanh, @madt, @ten, @diachi)

	UPDATE DOITAC
	SET SOCHINHANH = SOCHINHANH + 1
	WHERE MADT = @madt
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1
	END CATCH
COMMIT TRAN
	return 1
GO

--PROCEDURE khách hàng xem danh sách đối tác
CREATE PROCEDURE sp_KH_XemDSDoiTac
AS
SET TRAN ISOLATION LEVEL SERIALIZABLE
BEGIN TRAN
	BEGIN TRY
		SELECT TENDT,DIACHI,SOCHINHANH,LOAIHANG,SDT,MADT FROM DOITAC
		--ĐỂ TEST
		WAITFOR DELAY '0:0:10'
	END TRY
	BEGIN CATCH
			PRINT N'LỖI HỆ THỐNG'
			ROLLBACK TRAN
			RETURN 0
	END CATCH
COMMIT TRAN
return 1
GO
