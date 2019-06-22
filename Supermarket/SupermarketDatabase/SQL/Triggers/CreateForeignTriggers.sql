/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     1/11/2019 11:57:02 PM                        */
/*==============================================================*/


create trigger TD_CUSTOMER on CUSTOMER for delete as
begin
    declare
       @numrows  int,
       @errno    int,
       @errmsg   varchar(255)

    select  @numrows = @@rowcount
    if @numrows = 0
       return

    /*  Cannot delete parent "CUSTOMER" if children still exist in ""ORDER""  */
    if exists (select 1
               from   [ORDER] t2, deleted t1
               where  t2.CID = t1.CID)
       begin
          select @errno  = 50006,
                 @errmsg = 'Children still exist in ""ORDER"". Cannot delete parent "CUSTOMER".'
          goto error
       end


    return

/*  Errors handling  */
error:
    print @errmsg
    rollback  transaction
end
go


create trigger TU_CUSTOMER on CUSTOMER for update as
begin
   declare
      @numrows  int,
      @numnull  int,
      @errno    int,
      @errmsg   varchar(255)

      select  @numrows = @@rowcount
      if @numrows = 0
         return

      /*  Cannot modify parent code in "CUSTOMER" if children still exist in ""ORDER""  */
      if update(CID)
      begin
         if exists (select 1
                    from   [ORDER] t2, inserted i1, deleted d1
                    where  t2.CID = d1.CID
                     and  (i1.CID != d1.CID))
            begin
               select @errno  = 50005,
                      @errmsg = 'Children still exist in ""ORDER"". Cannot modify parent code in "CUSTOMER".'
               goto error
            end
      end


      return

/*  Errors handling  */
error:
    print @errmsg
    rollback  transaction
end
go




create trigger TD_EMPLOYEE on EMPLOYEE for delete as
begin
    declare
       @numrows  int,
       @errno    int,
       @errmsg   varchar(255)

    select  @numrows = @@rowcount
    if @numrows = 0
       return

    /*  Cannot delete parent "EMPLOYEE" if children still exist in ""ORDER""  */
    if exists (select 1
               from   [ORDER] t2, deleted t1
               where  t2.EID = t1.EID)
       begin
          select @errno  = 50006,
                 @errmsg = 'Children still exist in ""ORDER"". Cannot delete parent "EMPLOYEE".'
          goto error
       end


    return

/*  Errors handling  */
error:
     print @errmsg
    rollback  transaction
end
go


create trigger TU_EMPLOYEE on EMPLOYEE for update as
begin
   declare
      @numrows  int,
      @numnull  int,
      @errno    int,
      @errmsg   varchar(255)

      select  @numrows = @@rowcount
      if @numrows = 0
         return

      /*  Cannot modify parent code in "EMPLOYEE" if children still exist in ""ORDER""  */
      if update(EID)
      begin
         if exists (select 1
                    from   [ORDER] t2, inserted i1, deleted d1
                    where  t2.EID = d1.EID
                     and  (i1.EID != d1.EID))
            begin
               select @errno  = 50005,
                      @errmsg = 'Children still exist in ""ORDER"". Cannot modify parent code in "EMPLOYEE".'
               goto error
            end
      end


      return

/*  Errors handling  */
error:
    print @errmsg
    rollback  transaction
end
go





create trigger TI_ITEMS on ORDER_ITEMS for insert as
begin
    declare
       @numrows  int,
       @numnull  int,
       @errno    int,
       @errmsg   varchar(255)

    select  @numrows = @@rowcount
    if @numrows = 0
       return

    /*  Parent ""ORDER"" must exist when inserting a child in "ITEMS"  */
    if update(OID)
    begin
       if (select count(*)
           from   [ORDER] t1, inserted t2
           where  t1.OID = t2.OID) != @numrows
          begin
             select @errno  = 50002,
                    @errmsg = 'Parent does not exist in ""ORDER"". Cannot create child in "ITEMS".'
             goto error
          end
    end
    /*  Parent "PRODUCT" must exist when inserting a child in "ITEMS"  */
    if update(PID)
    begin
       if (select count(*)
           from   PRODUCT t1, inserted t2
           where  t1.PID = t2.PID) != @numrows
          begin
             select @errno  = 50002,
                    @errmsg = 'Parent does not exist in "PRODUCT". Cannot create child in "ITEMS".'
             goto error
          end
    end

    return

/*  Errors handling  */
error:
   print @errmsg
    rollback  transaction
end
go


create trigger TU_ITEMS on ORDER_ITEMS for update as
begin
   declare
      @numrows  int,
      @numnull  int,
      @errno    int,
      @errmsg   varchar(255)

      select  @numrows = @@rowcount
      if @numrows = 0
         return

      /*  Parent ""ORDER"" must exist when updating a child in "ITEMS"  */
      if update(OID)
      begin
         if (select count(*)
             from   [ORDER] t1, inserted t2
             where  t1.OID = t2.OID) != @numrows
            begin
               select @errno  = 50003,
                      @errmsg = '"ORDER"" does not exist. Cannot modify child in "ITEMS".'
               goto error
            end
      end
      /*  Parent "PRODUCT" must exist when updating a child in "ITEMS"  */
      if update(PID)
      begin
         if (select count(*)
             from   PRODUCT t1, inserted t2
             where  t1.PID = t2.PID) != @numrows
            begin
               select @errno  = 50003,
                      @errmsg = 'PRODUCT" does not exist. Cannot modify child in "ITEMS".'
               goto error
            end
      end

      return

/*  Errors handling  */
error:
    print @errmsg
    rollback  transaction
end
go





create trigger TD_ORDER on [ORDER] for delete as
begin
    declare
       @numrows  int,
       @errno    int,
       @errmsg   varchar(255)

    select  @numrows = @@rowcount
    if @numrows = 0
       return

    /*  Cannot delete parent ""ORDER"" if children still exist in "ITEMS"  */
    if exists (select 1
               from   ORDER_ITEMS t2, deleted t1
               where  t2.OID = t1.OID)
       begin
          select @errno  = 50006,
                 @errmsg = 'Children still exist in "ITEMS". Cannot delete parent ""ORDER"".'
          goto error
       end


    return

/*  Errors handling  */
error:
    print @errmsg
    rollback  transaction
end
go


create trigger TI_ORDER on [ORDER] for insert as
begin
    declare
       @numrows  int,
       @numnull  int,
       @errno    int,
       @errmsg   varchar(255)

    select  @numrows = @@rowcount
    if @numrows = 0
       return

    /*  Parent "CUSTOMER" must exist when inserting a child in ""ORDER""  */
    if update(CID)
    begin
       if (select count(*)
           from   CUSTOMER t1, inserted t2
           where  t1.CID = t2.CID) != @numrows
          begin
             select @errno  = 50002,
                    @errmsg = 'Parent does not exist in "CUSTOMER". Cannot create child in ""ORDER"".'
             goto error
          end
    end
    /*  Parent "EMPLOYEE" must exist when inserting a child in ""ORDER""  */
    if update(EID)
    begin
       if (select count(*)
           from   EMPLOYEE t1, inserted t2
           where  t1.EID = t2.EID) != @numrows
          begin
             select @errno  = 50002,
                    @errmsg = 'Parent does not exist in "EMPLOYEE". Cannot create child in ""ORDER"".'
             goto error
          end
    end

    return

/*  Errors handling  */
error:
    print @errmsg
    rollback  transaction
end
go


create trigger TU_ORDER on [ORDER] for update as
begin
   declare
      @numrows  int,
      @numnull  int,
      @errno    int,
      @errmsg   varchar(255)

      select  @numrows = @@rowcount
      if @numrows = 0
         return

      /*  Parent "CUSTOMER" must exist when updating a child in ""ORDER""  */
      if update(CID)
      begin
         if (select count(*)
             from   CUSTOMER t1, inserted t2
             where  t1.CID = t2.CID) != @numrows
            begin
               select @errno  = 50003,
                      @errmsg = 'CUSTOMER" does not exist. Cannot modify child in ""ORDER"".'
               goto error
            end
      end
      /*  Parent "EMPLOYEE" must exist when updating a child in ""ORDER""  */
      if update(EID)
      begin
         if (select count(*)
             from   EMPLOYEE t1, inserted t2
             where  t1.EID = t2.EID) != @numrows
            begin
               select @errno  = 50003,
                      @errmsg = 'EMPLOYEE" does not exist. Cannot modify child in ""ORDER"".'
               goto error
            end
      end
      /*  Cannot modify parent code in ""ORDER"" if children still exist in "ITEMS"  */
      if update(OID)
      begin
         if exists (select 1
                    from   ORDER_ITEMS t2, inserted i1, deleted d1
                    where  t2.OID = d1.OID
                     and  (i1.OID != d1.OID))
            begin
               select @errno  = 50005,
                      @errmsg = 'Children still exist in "ITEMS". Cannot modify parent code in ""ORDER"".'
               goto error
            end
      end


      return

/*  Errors handling  */
error:
    print @errmsg
    rollback  transaction
end
go





create trigger TD_PRODUCT on PRODUCT for delete as
begin
    declare
       @numrows  int,
       @errno    int,
       @errmsg   varchar(255)

    select  @numrows = @@rowcount
    if @numrows = 0
       return

    /*  Cannot delete parent "PRODUCT" if children still exist in "ITEMS"  */
    if exists (select 1
               from   ORDER_ITEMS t2, deleted t1
               where  t2.PID = t1.PID)
       begin
          select @errno  = 50006,
                 @errmsg = 'Children still exist in "ITEMS". Cannot delete parent "PRODUCT".'
          goto error
       end

    /*  Cannot delete parent "PRODUCT" if children still exist in "STOCK"  */
    if exists (select 1
               from   STOCK t2, deleted t1
               where  t2.PID = t1.PID)
       begin
          select @errno  = 50006,
                 @errmsg = 'Children still exist in "STOCK". Cannot delete parent "PRODUCT".'
          goto error
       end

    /*  Cannot delete parent "PRODUCT" if children still exist in "SUPPLIER_PRODUCT"  */
    if exists (select 1
               from   SUPPLIER_PRODUCT t2, deleted t1
               where  t2.PID = t1.PID)
       begin
          select @errno  = 50006,
                 @errmsg = 'Children still exist in "SUPPLIER_PRODUCT". Cannot delete parent "PRODUCT".'
          goto error
       end


    return

/*  Errors handling  */
error:
   print @errmsg
    rollback  transaction
end
go


create trigger TU_PRODUCT on PRODUCT for update as
begin
   declare
      @numrows  int,
      @numnull  int,
      @errno    int,
      @errmsg   varchar(255)

      select  @numrows = @@rowcount
      if @numrows = 0
         return

      /*  Cannot modify parent code in "PRODUCT" if children still exist in "ITEMS"  */
      if update(PID)
      begin
         if exists (select 1
                    from   ORDER_ITEMS t2, inserted i1, deleted d1
                    where  t2.PID = d1.PID
                     and  (i1.PID != d1.PID))
            begin
               select @errno  = 50005,
                      @errmsg = 'Children still exist in "ITEMS". Cannot modify parent code in "PRODUCT".'
               goto error
            end
      end

      /*  Cannot modify parent code in "PRODUCT" if children still exist in "STOCK"  */
      if update(PID)
      begin
         if exists (select 1
                    from   STOCK t2, inserted i1, deleted d1
                    where  t2.PID = d1.PID
                     and  (i1.PID != d1.PID))
            begin
               select @errno  = 50005,
                      @errmsg = 'Children still exist in "STOCK". Cannot modify parent code in "PRODUCT".'
               goto error
            end
      end

      /*  Cannot modify parent code in "PRODUCT" if children still exist in "SUPPLIER_PRODUCT"  */
      if update(PID)
      begin
         if exists (select 1
                    from   SUPPLIER_PRODUCT t2, inserted i1, deleted d1
                    where  t2.PID = d1.PID
                     and  (i1.PID != d1.PID))
            begin
               select @errno  = 50005,
                      @errmsg = 'Children still exist in "SUPPLIER_PRODUCT". Cannot modify parent code in "PRODUCT".'
               goto error
            end
      end


      return

/*  Errors handling  */
error:
   print @errmsg
    rollback  transaction
end
go




create trigger TI_STOCK on STOCK for insert as
begin
    declare
       @maxcard  int,
       @numrows  int,
       @numnull  int,
       @errno    int,
       @errmsg   varchar(255)

    select  @numrows = @@rowcount
    if @numrows = 0
       return

    /*  Parent "PRODUCT" must exist when inserting a child in "STOCK"  */
    if update(PID)
    begin
       if (select count(*)
           from   PRODUCT t1, inserted t2
           where  t1.PID = t2.PID) != @numrows
          begin
             select @errno  = 50002,
                    @errmsg = 'Parent does not exist in "PRODUCT". Cannot create child in "STOCK".'
             goto error
          end
    end
    /*  The cardinality of Parent "PRODUCT" in child "STOCK" cannot exceed 1 */
    if update(PID)
    begin
       select @maxcard = (select count(*)
          from   STOCK old
          where ins.PID = old.PID)
       from  inserted ins
       where ins.PID is not null
       group by ins.PID
       order by 1
       if @maxcard > 1
       begin
          select @errno  = 50007,
                 @errmsg = 'The maximum cardinality of a child has been exceeded! Cannot create child in "STOCK".'
          goto error
       end
    end

    return

/*  Errors handling  */
error:
    print @errmsg
    rollback  transaction
end
go


create trigger TU_STOCK on STOCK for update as
begin
   declare
      @maxcard  int,
      @numrows  int,
      @numnull  int,
      @errno    int,
      @errmsg   varchar(255)

      select  @numrows = @@rowcount
      if @numrows = 0
         return

      /*  Parent "PRODUCT" must exist when updating a child in "STOCK"  */
      if update(PID)
      begin
         if (select count(*)
             from   PRODUCT t1, inserted t2
             where  t1.PID = t2.PID) != @numrows
            begin
               select @errno  = 50003,
                      @errmsg = 'PRODUCT" does not exist. Cannot modify child in "STOCK".'
               goto error
            end
      end
      /*  The cardinality of Parent "PRODUCT" in child "STOCK" cannot exceed 1 */
      if update(PID)
      begin
         select @maxcard = (select count(*)
            from   STOCK old
            where ins.PID = old.PID)
         from  inserted ins
         where ins.PID is not null
         group by ins.PID
         order by 1
         if @maxcard > 1
         begin
            select @errno  = 50007,
                   @errmsg = 'The maximum cardinality of a child has been exceeded! Cannot modify child in "STOCK".'
            goto error
         end
      end

      return

/*  Errors handling  */
error:
    print @errmsg
    rollback  transaction
end
go





create trigger TD_SUPPLIER on SUPPLIER for delete as
begin
    declare
       @numrows  int,
       @errno    int,
       @errmsg   varchar(255)

    select  @numrows = @@rowcount
    if @numrows = 0
       return

    /*  Cannot delete parent "SUPPLIER" if children still exist in "SUPPLIER_PRODUCT"  */
    if exists (select 1
               from   SUPPLIER_PRODUCT t2, deleted t1
               where  t2.SUPPID = t1.SUPPID)
       begin
          select @errno  = 50006,
                 @errmsg = 'Children still exist in "SUPPLIER_PRODUCT". Cannot delete parent "SUPPLIER".'
          goto error
       end


    return

/*  Errors handling  */
error:
    print @errmsg
    rollback  transaction
end
go


create trigger TU_SUPPLIER on SUPPLIER for update as
begin
   declare
      @numrows  int,
      @numnull  int,
      @errno    int,
      @errmsg   varchar(255)

      select  @numrows = @@rowcount
      if @numrows = 0
         return

      /*  Cannot modify parent code in "SUPPLIER" if children still exist in "SUPPLIER_PRODUCT"  */
      if update(SUPPID)
      begin
         if exists (select 1
                    from   SUPPLIER_PRODUCT t2, inserted i1, deleted d1
                    where  t2.SUPPID = d1.SUPPID
                     and  (i1.SUPPID != d1.SUPPID))
            begin
               select @errno  = 50005,
                      @errmsg = 'Children still exist in "SUPPLIER_PRODUCT". Cannot modify parent code in "SUPPLIER".'
               goto error
            end
      end


      return

/*  Errors handling  */
error:
    print @errmsg
    rollback  transaction
end
go





create trigger TI_SUPPLIER_PRODUCT on SUPPLIER_PRODUCT for insert as
begin
    declare
       @numrows  int,
       @numnull  int,
       @errno    int,
       @errmsg   varchar(255)

    select  @numrows = @@rowcount
    if @numrows = 0
       return

    /*  Parent "SUPPLIER" must exist when inserting a child in "SUPPLIER_PRODUCT"  */
    if update(SUPPID)
    begin
       if (select count(*)
           from   SUPPLIER t1, inserted t2
           where  t1.SUPPID = t2.SUPPID) != @numrows
          begin
             select @errno  = 50002,
                    @errmsg = 'Parent does not exist in "SUPPLIER". Cannot create child in "SUPPLIER_PRODUCT".'
             goto error
          end
    end
    /*  Parent "PRODUCT" must exist when inserting a child in "SUPPLIER_PRODUCT"  */
    if update(PID)
    begin
       if (select count(*)
           from   PRODUCT t1, inserted t2
           where  t1.PID = t2.PID) != @numrows
          begin
             select @errno  = 50002,
                    @errmsg = 'Parent does not exist in "PRODUCT". Cannot create child in "SUPPLIER_PRODUCT".'
             goto error
          end
    end

    return

/*  Errors handling  */
error:
   print @errmsg
    rollback  transaction
end
go


create trigger TU_SUPPLIER_PRODUCT on SUPPLIER_PRODUCT for update as
begin
   declare
      @numrows  int,
      @numnull  int,
      @errno    int,
      @errmsg   varchar(255)

      select  @numrows = @@rowcount
      if @numrows = 0
         return

      /*  Parent "SUPPLIER" must exist when updating a child in "SUPPLIER_PRODUCT"  */
      if update(SUPPID)
      begin
         if (select count(*)
             from   SUPPLIER t1, inserted t2
             where  t1.SUPPID = t2.SUPPID) != @numrows
            begin
               select @errno  = 50003,
                      @errmsg = 'SUPPLIER" does not exist. Cannot modify child in "SUPPLIER_PRODUCT".'
               goto error
            end
      end
      /*  Parent "PRODUCT" must exist when updating a child in "SUPPLIER_PRODUCT"  */
      if update(PID)
      begin
         if (select count(*)
             from   PRODUCT t1, inserted t2
             where  t1.PID = t2.PID) != @numrows
            begin
               select @errno  = 50003,
                      @errmsg = 'PRODUCT" does not exist. Cannot modify child in "SUPPLIER_PRODUCT".'
               goto error
            end
      end

      return

/*  Errors handling  */
error:
    print @errmsg
    rollback  transaction
end
go

