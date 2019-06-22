--drop view Order_Products_ExtraInformation

Create View Order_Products_ExtraInformation 
as
Select O.OID,QUANTITY,PNAME,PRICE,PRICE_Currancy,Current_Price,Date
from ORDER_ITEMS as O , PRODUCT as P,[Order]
where O.PID=P.PID
and   O.OID=[Order].OID



