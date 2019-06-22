--drop view Order_Products
Create View Order_Products 
as
Select OID,QUANTITY,PNAME,PRICE,PRICE_Currancy
from ORDER_ITEMS as O , PRODUCT as P
where O.PID=P.PID

--Select * from Order_Products