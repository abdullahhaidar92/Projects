
/*==============================================================*/
/* Table: CUSTOMER                                              */
/*==============================================================*/
/*
drop table SUPPLIER_PRODUCT
drop table STOCK
drop table SUPPLIER
drop table ORDER_ITEMS 
drop table PRODUCT
drop table "ORDER" 
drop table EMPLOYEE
drop table CUSTOMER
*/
create table CUSTOMER 
(
   CID                  char(5)                   not null,
   FNAME                char(20)                       null,
   LNAME                char(20)                       null,
   BIRTHDATE            date                           null,
   ADDRESS              char(20)                       null,
   PHONE                char(10)                       null,
   GENDER               char(10)                       null,
   constraint PK_CUSTOMER primary key (CID)
);

/*==============================================================*/
/* Table: EMPLOYEE                                              */
/*==============================================================*/
create table EMPLOYEE 
(
   EID                  char(5)                    not null,
   FNAME                char(20)                       null,
   LNAME                char(20)                       null,
   BIRTHDATE            date                           null,
   ADDRESS              char(20)                       null,
   PHONE                char(10)                       null,
   GENDER               char(10)                       null,
   SALARY               float                          null,
   SALARY_Currancy       char(5)                        null,
   Position				char(20)					      null,
   constraint PK_EMPLOYEE primary key (EID)
);

/*==============================================================*/
/* Table: "ORDER"                                               */
/*==============================================================*/

create table [ORDER] 
(
   OID                  int                          not null,
   CID                  char(5)                        not null,
   EID                  char(5)                        not null,
   TOTAL                float                          null,
   TOTAL_Currancy        char(5)                        null,
   DATE               datetime                        null,
   constraint PK_ORDER primary key (OID),
 
        
);

/*==============================================================*/
/* Table: PRODUCT                                               */
/*==============================================================*/
--drop table PRODUCT 
--drop table ITEMS 
--drop table STOCK
--drop table SUPPLIER
--drop table SUPPLIER_PRODUCT
create table PRODUCT 
(
   PID                  char(5)                   not null,
   PNAME                char(20)                       null,
   CATEGORY             char(10)                       null,
   PRICE                float                          null,
   PRICE_Currancy       char(5)                        null,
   EXPIRAYDATE          date                            null,
   TVA 					float							null,
   BARCODE              char(20)                       null,
   IMAGE                char(10)                       null,
   Current_PRICE                float                          null,
   constraint PK_PRODUCT primary key (PID)
);


/*==============================================================*/
/* Table: ITEMS                                                 */
/*==============================================================*/

create table ORDER_ITEMS 
(
   OID                  int                        not null,
   PID                  char(5)                       not null,
   QUANTITY             integer                        null,
   constraint PK_ITEMS primary key clustered (OID, PID),
   
  
);

/*==============================================================*/
/* Table: STOCK                                                 */
/*==============================================================*/
--drop table STOCK
--drop table SUPPLIER
--drop table SUPPLIER_PRODUCT 
create table STOCK 
(
   SID                  char(5)                    not null,
   PID                  char(5)                    not null,
   QUANTITY             integer                        null,
   MIN                  integer                        null,
   MAX                  integer                        null,
   constraint PK_STOCK primary key (SID),
   
);

/*==============================================================*/
/* Table: SUPPLIER                                              */
/*==============================================================*/
--drop table SUPPLIER
--drop table SUPPLIER_PRODUCT 
create table SUPPLIER 
(
   SUPPID               char(5)                    not null,
   FNAME                char(20)                       null,
   LNAME                char(20)                       null,
   ADDRESS              char(20)                       null,
   PHONE                char(10)                       null,
   GENDER               char(10)                       null,
   COMPANY              char(100)                       null,
   constraint PK_SUPPLIER primary key (SUPPID)
);

/*==============================================================*/
/* Table: SUPPLIER_PRODUCT                                      */
/*==============================================================*/
--drop table SUPPLIER_PRODUCT 
create table SUPPLIER_PRODUCT 
(
   SUPPID               char(5)                        not null,
   PID                  char(5)                        not null,
   PRICE         float                          null,
   PRICE_Currancy       char(5)                        null,
   constraint PK_SUPPLIER_PRODUCT primary key clustered (SUPPID, PID),

     
);

