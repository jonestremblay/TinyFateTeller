/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ca.jonestremblay.models;

import java.time.LocalDate;
import java.util.Date;

/**
 *
 * @author jonat
 */
public class Utilisateur {
    private String UserName;
    private String HostName;
    private String LOCAL_IP_Address;
    private String PUBLIC_IP_Address;
    private String EntryDate;
    private LocalDate BirthDate;
    
    public Utilisateur(){
        
    }
    
    public Utilisateur(String UserName, String HostName, String LOCAL_IP_Address, String PUBLIC_IP_Address, String EntryDate, LocalDate BirthDate) {
        this.UserName = UserName;
        this.HostName = HostName;
        this.LOCAL_IP_Address = LOCAL_IP_Address;
        this.PUBLIC_IP_Address = PUBLIC_IP_Address;
        this.EntryDate = EntryDate;
        this.BirthDate = BirthDate;
    }

    public String getUserName() {
        return UserName;
    }

    public void setUserName(String UserName) {
        this.UserName = UserName;
    }

    public String getHostName() {
        return HostName;
    }

    public void setHostName(String HostName) {
        this.HostName = HostName;
    }

    public String getLOCAL_IP_Address() {
        return LOCAL_IP_Address;
    }

    public void setLOCAL_IP_Address(String LOCAL_IP_Address) {
        this.LOCAL_IP_Address = LOCAL_IP_Address;
    }

    public String getPUBLIC_IP_Address() {
        return PUBLIC_IP_Address;
    }

    public void setPUBLIC_IP_Address(String PUBLIC_IP_Address) {
        this.PUBLIC_IP_Address = PUBLIC_IP_Address;
    }

    public String getEntryDate() {
        return EntryDate;
    }

    public void setEntryDate(String EntryDate) {
        this.EntryDate = EntryDate;
    }

    public LocalDate getBirthDate() {
        return BirthDate;
    }

    public void setBirthDate(LocalDate BirthDate) {
        this.BirthDate = BirthDate;
    }
    
    public String ToString()
    {
        return this.UserName + " \n"
            + "\t Date of birth : " + this.BirthDate + "\n"
            + "\t Hostname | Local IP : " + this.HostName + " | " + this.LOCAL_IP_Address
            + "\t Public IP | Date the user has been added : " + this.PUBLIC_IP_Address + " | " + this.EntryDate;
    }
}
