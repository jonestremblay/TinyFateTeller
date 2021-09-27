/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ca.jonestremblay.utils;

import com.google.gson.Gson;
import com.profesorfalken.jpowershell.PowerShell;
import com.profesorfalken.jpowershell.PowerShellResponse;
import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.Console;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.InetAddress;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;
import java.net.URLEncoder;
import java.net.UnknownHostException;
import java.sql.Timestamp;
import java.util.Locale;
import java.util.Scanner;
import ca.jonestremblay.models.Utilisateur;
import net.sf.json.JSONException;

/**
 *
 * @author jonat
 */
public abstract class UserTracking {

    public enum OSType {
        Windows, MacOS, Linux, Other;
        public static final OSType DETECTED;

        static {
            String OS = System.getProperty("os.name", "generic").toLowerCase(Locale.ENGLISH);
            if ((OS.contains("mac")) || (OS.contains("darwin"))) {
                DETECTED = OSType.MacOS;
            } else if (OS.contains("win")) {
                DETECTED = OSType.Windows;
            } else if (OS.contains("nux")) {
                DETECTED = OSType.Linux;
            } else {
                DETECTED = OSType.Other;
            }
        }
    }

    public static String getClientHostname() {
        InetAddress ip;
        String hostname = "";
        try {
            ip = InetAddress.getLocalHost();
            hostname = ip.getHostName();
        } catch (UnknownHostException e) {
            e.printStackTrace();
        }
        return hostname;
    }

    public static String getClientLocalIpAddress() {
        InetAddress ip = null;
        try {
            ip = InetAddress.getLocalHost();
        } catch (UnknownHostException e) {
            e.printStackTrace();
        }
        return ip.getHostAddress();
    }

    public static Timestamp getTimestamp() {
        return new Timestamp(System.currentTimeMillis());
    }

    public static String getClientPublicIPAddress() {
        switch (OSType.DETECTED) {
            case Windows:
                return getClientPublicIPAddressFromWindows();
            default:
                return getClientPublicIPAddressFromUNIX();
        }
    }

    public static String getClientPublicIPAddressFromWindows() {
        PowerShellResponse response = PowerShell.executeSingleCommand("(Invoke-WebRequest ifconfig.me/ip).Content.Trim()");
        return response.getCommandOutput();
    }

    public static String getClientPublicIPAddressFromUNIX() {
        ProcessBuilder processBuilder = new ProcessBuilder();
        processBuilder.command("bash", "-c", "dig +short myip.opendns.com @resolver1.opendns.com");
        String ipAddress = "";
        try {
            Process process = processBuilder.start();
            BufferedReader reader
                    = new BufferedReader(new InputStreamReader(process.getInputStream()));
            String line;
            while ((line = reader.readLine()) != null) {
                ipAddress = line;
            }
            int exitCode = process.waitFor();
            System.out.println("\nExited with error code : " + exitCode);
        } catch (IOException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return ipAddress;
    }

}
