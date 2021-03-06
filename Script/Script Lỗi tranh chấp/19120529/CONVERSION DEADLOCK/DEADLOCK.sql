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
