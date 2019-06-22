--drop Procedure AddItemToOrder
Create Procedure AddItemToOrder @OID int ,@PID char(5),@QUANTITY int
AS
Begin
Declare @currency char(5),
	   @total float,
	   @price float,
	   @tva float
Insert into ORDER_ITEMS 
values(@OID,@PID,@QUANTITY)
set @currency=(Select PRICE_Currancy from PRODUCT where PID=@PID)
set @price=(Select PRICE from PRODUCT where PID=@PID)
set @tva=(Select TVA from PRODUCT where PID=@PID)
if(@currency like '$')
begin
set @price=@price*1500
end
set @price=@price+@price*@tva
set @price=@price*@QUANTITY
Update [ORDER]
set TOTAL=TOTAL+@price
End