--drop procedure AddSupplier
Create Procedure AddSupplier @SUPPID char(5) ,                
                           @FNAME  char(20),
                           @LNAME  char(20),
                           @ADDRESS char(20),   
                           @PHONE    char(10) ,
                           @GENDER   char(10),  
                           @COMPANY  char(20)

As
Begin
Insert into SUPPLIER values(@SUPPID,@FNAME,@LNAME,@ADDRESS,@PHONE,@GENDER,@COMPANY);
End






