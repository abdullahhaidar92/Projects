--drop procedure getNbOrders 

--returns the number of the revenue made
--in a day, month and year 
Create Procedure getNbOrders
as
begin
declare @date1 Date;
declare @date2 Date;
declare @daily integer;
declare @monthly integer;
declare @yearly integer;
set @date2=(select GETDATE())

set @date1=(select GETDATE()-1)
set @daily = (select count(OID) as total
             from OrdersView
             where date between @date1 and @date2 
		     )
		   


set @date1=(select GETDATE()-30)
set @monthly = (select count(OID) as total
             from OrdersView
             where date between @date1 and @date2 
		       )



set @date1=(select GETDATE()-256)
set @yearly =(select count(*) as total
             from OrdersView
             where date between @date1 and @date2 
		     )

select @daily as daily ,@monthly as monthly,@yearly as yearly

end

---exec getNbOrders

