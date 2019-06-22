--drop procedure getRevenue  
Create Procedure getRevenue @date1 Date , @date2 Date
as
begin
Select sum((PRICE-Current_Price)*Quantity) as total 
from Order_Products_ExtraInformation
where date between @date1 and @date2 
end
