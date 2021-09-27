/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ca.jonestremblay.services;

import ca.jonestremblay.models.Utilisateur;
import com.google.gson.Gson;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;
import java.net.URLEncoder;

/**
 *
 * @author jonat
 */
public abstract class BirthService {
     public static String AddUserToDatabase(Utilisateur user) throws UnsupportedEncodingException, MalformedURLException, ProtocolException, IOException {
        String charset = "UTF-8";
        String query = String.format(
                "Username=%s&Hostname=%s&LocalIP=%s&PublicIP=%s&EntryDate=%s&BirthDate=%s",
                URLEncoder.encode(user.getUserName(), charset),
                URLEncoder.encode(user.getHostName(), charset),
                URLEncoder.encode(user.getLOCAL_IP_Address(), charset),
                URLEncoder.encode(user.getPUBLIC_IP_Address(), charset),
                URLEncoder.encode(user.getEntryDate(), charset),
                URLEncoder.encode(user.getBirthDate().toString(), charset));
        HttpURLConnection connection
                = (HttpURLConnection) new URL(
                        "http://localhost:5000/api/Birth/AddUserToDatabase/?" + query)
                        .openConnection();
        connection.setRequestProperty("Accept-Charset", charset);
        connection.setRequestProperty("Content-Type", "application/json; charset=utf-8");
        connection.setRequestProperty("Content-Length", "0");
        connection.setRequestProperty("Accept", "application/json");
        connection.setRequestMethod("POST");
        connection.setDoOutput(true);
        String json = new Gson().toJson(user);
        try (OutputStream os = connection.getOutputStream()) {
            byte[] input = json.getBytes("utf-8");
            os.write(input, 0, input.length);
        }
        
        StringBuilder response;
        try (BufferedReader br = new BufferedReader(
                new InputStreamReader(connection.getInputStream(), "utf-8"))) {
            response = new StringBuilder();
            String responseLine = null;
            while ((responseLine = br.readLine()) != null) {
                response.append(responseLine.trim());
            }
        }
        return response.toString();
    }

    public static String getActivite(String birthDate) throws IOException {
        String charset = "UTF-8";
        String query = String.format("BirthDate=%s", URLEncoder.encode(
                birthDate, charset));
        HttpURLConnection connection
                = (HttpURLConnection) new URL(
                        "http://localhost:5000/api/Birth/getActivity?" + query)
                        .openConnection();
        connection.setRequestProperty("Accept-Charset", charset);
        connection.setRequestMethod("GET");
        int responseCode = connection.getResponseCode();
        BufferedReader br = null;
        String result = "";
        if (responseCode == 200) {
            try {
                br = new BufferedReader(new InputStreamReader(
                        connection.getInputStream(), "UTF-8"));
                String inputLine;
                while ((inputLine = br.readLine()) != null) {
                    result = inputLine;
                }
            } catch (Exception ex) {
                System.out.println(ex.getMessage());
            } finally {
                if (br != null) {
                    br.close();
                }
            }
        } else {
            result = "An error occured. ";
        }
        return result;
    }
}
