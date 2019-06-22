Create Procedure AddSupplierToProduct @SUPPID Char(5),
									@PID   Char(5),
									@PRICE float,
									@PRICE_CURRANCY char(5)

As
Begin
Insert Into SUPPLIER_PRODUCT values(@SUPPID,@PID,@PRICE,@PRICE_CURRANCY)
End