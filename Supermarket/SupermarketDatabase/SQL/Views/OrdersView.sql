--drop View OrdersView
Create View OrdersView 
as Select OID,O.CID,O.EID,TOTAL,TOTAL_Currancy,DATE,
	     E.FNAME as EmpFNAME,E.LNAME as EmpLNAME,
		 C.FNAME as CusFNAME,C.LNAME as CusLNAME
   from [Order] as O, Employee as E  ,Customer as C 
   where C.CID=O.CID
     and O.EID=E.EID

--select * from SUPPLIER where SUPPID='SP100'  
