use QL_DH_GH
DECLARE @MACHINHANH VARCHAR(15), @MADT VARCHAR(15), @DIACHI NVARCHAR(50), @ANS INT, @TEN NVARCHAR(50)
SET @MACHINHANH = 'CN12'
SET @MADT = 'DT111'
SET @DIACHI = N'Q12, Tp. Hồ Chí Minh'
SET @TEN = N'Bình Minh'
EXEC @ANS = sp_DT_ThemChiNhanh @MADT, @MACHINHANH, @DIACHI, @TEN 
PRINT @ANS
