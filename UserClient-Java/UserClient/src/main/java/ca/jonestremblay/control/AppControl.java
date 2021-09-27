package ca.jonestremblay.control;

import ca.jonestremblay.ui.FenGUI;
import ca.jonestremblay.ui.FenSplash;

public class AppControl {

    public static void main(String[] args) {
        FenGUI fenGUI = new FenGUI();
        FenSplash splashScreen = new FenSplash(fenGUI);
        fenGUI.setVisible(false);
        splashScreen.dispose();
        fenGUI.setVisible(true);
    }
}