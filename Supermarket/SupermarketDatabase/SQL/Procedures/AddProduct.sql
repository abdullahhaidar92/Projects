--use SuperMarket4
--drop Procedure AddProduct
Create Procedure AddProduct  @PID char(5),
						   @PNAME char(20),                    
						   @CATEGORY char(10), 
						   @PRICE float,
						   @PRICE_Currancy char(5),
						   @EXPIRAYDATE date,
						   @TVA float,
						   @BARCODE char(20),
						   @IMAGE char(10)
						   
As
Begin
Insert into PRODUCT values(@PID,@PNAME,@CATEGORY,@PRICE,@PRICE_Currancy,@EXPIRAYDATE,@TVA,@BARCODE,@IMAGE,0);
End
