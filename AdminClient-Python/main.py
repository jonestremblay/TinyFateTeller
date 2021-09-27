import PySimpleGUI as sg
from modules import gui as gui
from modules import gui_layout as layout

splashWindow = gui.get_splash_window()
window = gui.get_main_window()


# Show splash window
window.hide()
for frame in layout.sequence:
        event, values = splashWindow.read(timeout=layout.interframe_duration)
        splashWindow['-IMAGE-'].update(data=frame)
        if event == sg.WIN_CLOSED:
            break
window.UnHide()

while True: 
    event, values = gui.get_main_window().read(timeout=1000)
    gui.update_event_and_values(event, values)
    print("currently checking for events...")
    if event in (sg.WIN_CLOSED, 'Exit'):
        break
    # Lorsqu'on clique sur une option de tri
    if event in layout.sortingKeys:
        gui.handle_sorting(event)
    # Lorsqu'on clique sur 'Voir tous les utilisateurs'
    if event == "-btn_getAll-":
        gui.get_all_users_in_table()
    # Lorsqu'on clique sur 'Rechercher par username'
    if event == "-btn_search_username-":
        gui.handle_username_search(values)
    # Lorsqu'on clique sur 'Rechercher par IP publique'
    if event == "-btn_search_public_ip-":
        gui.handle_public_ip_search(values)
    # Lorsqu'on clique sur 'Filtrer par date d'entrée'
    if event == "-btn_filtre_entryDate-":
        gui.handle_entry_date_filters(values)
    # Lorsqu'on clique sur 'Filtrer par date de naissance'
    if event == "-btn_filtre_birthDate-":
        if gui.handle_birth_date_filters() is True:
            date = window["-DATE_NAISSANCE-"].get()
    # Lorsqu'on clique sur 'Save'
    if event == "-btn_save-":
        gui.save_table_data_in_csv_file()
    # Lorsqu'on clique sur envoyer avec email par défaut 
    if event == "... le email par défaut":
        gui.mail.launch_default_email_sender()
    # Lorsqu'on clique sur envoyer avec email personnel 
    if event == "... mon email personnel":
        gui.mail.launch_email_sender()
    # Lorsqu'on séléctionne le radio de filtre de date d'entrée
    if values["-RADIO_DROPLIST-"]: # Si le radio avec dropdown est selected
        gui.clean_inputs("-DATE_ENTREE_LIMITE_1-", "-DATE_ENTREE_LIMITE_2-")
    if values["-RADIO_CALENDAR-"]: # Si le radio avec btnCalendar est selected
        gui.clean_inputs("-COMBO_FILTRE_2-")
    

window.close()
