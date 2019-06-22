package model;

import java.sql.Connection;
import java.util.ArrayList;



public abstract class Employee {
	private String id;
	 private String fname;
	 private String lname;
	 private  String birthdate;
	 private  String address;
	 private  String phone;
	 private  String gender;
	 protected double salary;
	private String Sarlary_Currancy;
	private String Position;
	private String username;
	private String password;
	protected Connection con;
    protected double sum=0;
	 protected ArrayList<OrderTableEntry> Orders;
	
	public void setOrders(ArrayList<OrderTableEntry> orders) {
		Orders = orders;
	}
	public Connection getCon() {
		return con;
	}
	public void setCon(Connection con) {
		this.con = con;
	//	System.out.println("Connection set for Employee "+this.fname);
	}
	public String getUsername() {
		return username;
	}
	public void setUsername(String username) {
		this.username = username;
	}
	public String getPassword() {
		return password;
	}
	public void setPassword(String password) {
		this.password = password;
	}
	public String getId() {
		return id;
	}
	public void setId(String id) {
		this.id = id;
	}
	public String getFname() {
		return fname;
	}
	public void setFname(String fname) {
		this.fname = fname;
	}
	public String getLname() {
		return lname;
	}
	public void setLname(String lname) {
		this.lname = lname;
	}
	public String getBirthdate() {
		return birthdate;
	}
	public void setBirthdate(String birthdate) {
		this.birthdate = birthdate;
	}
	public String getAddress() {
		return address;
	}
	public void setAddress(String address) {
		this.address = address;
	}
	public String getPhone() {
		return phone;
	}
	public void setPhone(String phone) {
		this.phone = phone;
	}
	public String getGender() {
		return gender;
	}
	public void setGender(String gender) {
		this.gender = gender;
	}
	
	public void setSalary(double salary) {
		this.salary = salary;
	}
	public String getSarlary_Currancy() {
		return Sarlary_Currancy;
	}
	public void setSarlary_Currancy(String sarlary_Currancy) {
		Sarlary_Currancy = sarlary_Currancy;
	}
	public String getPosition() {
		return Position;
	}
	public void setPosition(String position) {
		Position = position;
	}
	public abstract double getSalary() ;
	public  abstract ArrayList<OrderTableEntry> getOrders();
	public abstract void createAccount(String username,String password);
	public abstract void grantPermissions() ;
	public abstract void configureDetails();
	 

}
