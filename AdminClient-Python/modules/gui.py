import PySimpleGUI as sg
import csv
from . import database as db
from . import gui_layout as layout
from . import gui_validator as validator
from . import email_utils as mail
 
splashWindow = layout.get_splash_window()
window = layout.get_main_window()
event = ""
values = ""

def update_event_and_values(window_event, window_values):
    event = window_event
    values = window_values

def get_splash_window():
    return splashWindow

def get_main_window():
    return window

def filter_by_birth_date(when):
    """ Searches users by [dateOfBirth]
         [when] : {"exactement", "avant", "après"} """
    
    filteredResults = []
    def get_date():
        return window["-DATE_NAISSANCE-"].get()

    if when == "exactement":
        print("Date de naissance : ", get_date())
        filteredResults = db.get_all_by_birth_date(get_date())
    elif when == "avant":
        print("Date de naissance : ", get_date())
        filteredResults = db.get_all_before_birth_date(get_date())
    elif when == "après":
        print("Date de naissance : ", get_date())
        filteredResults = db.get_all_after_birth_date(get_date())
    return filteredResults


def filter_by_entry_date(option, values):
    """ Searches users with the entryDate between [entryDate_1] and [entryDate_2], 
        if ([entryDate_1] and [entryDate_2]) was given"""
    
    filteredResults = []
    def get_starting_entry_date():
        return window["-DATE_ENTREE_LIMITE_1-"].get()
    def get_ending_entry_date():
        return window["-DATE_ENTREE_LIMITE_2-"].get()
    def get_timeline_option():
        return values["-COMBO_FILTRE_2-"]

    if option == "droplist_range":
        timeline = convert_timeline_option(get_timeline_option())
        filteredResults = db.get_all_in_entry_date_range(timeline)
    elif option == "calendar_range":
        filteredResults = db.get_all_between_two_entry_date(
                            get_starting_entry_date(), get_ending_entry_date())
    return filteredResults

def convert_timeline_option(pickedTimeline):
    """ Converts dropdown's option for the API."""
    if pickedTimeline == layout.optionsFiltre2[0]:
        return "today"
    elif pickedTimeline == layout.optionsFiltre2[1]:
        return "this_week"
    elif pickedTimeline == layout.optionsFiltre2[2]:
        return "this_month"
    elif pickedTimeline == layout.optionsFiltre2[3]:
        return "last_3_months"
    elif pickedTimeline == layout.optionsFiltre2[4]:
        return "last_6_months"
    elif pickedTimeline == layout.optionsFiltre2[5]:
        return "this_year"
    else:
        return ""

def get_entry_date_option(values):
    if values["-RADIO_DROPLIST-"] is True:
        return "droplist_range"
    elif values["-RADIO_CALENDAR-"] is True:
        return "calendar_range"

def handle_entry_date_filters(values):
    """ Searches users by [entryDate], if [entryDate] was given"""
    handled = False
    option = get_entry_date_option(values)
    if option == "droplist_range":
        if validator.check_dropdown_options("-COMBO_FILTRE_2-") is True:
            data = filter_by_entry_date(option, values)
            window['-TABLE-'].update(values=data)
            clean_inputs("-COMBO_FILTRE_2-")
            handled = True
    elif option == "calendar_range":
        if validator.check_if_entry_dates_were_given() is True:
            data = filter_by_entry_date(option)
            window['-TABLE-'].update(values=data)
            clean_inputs("-DATE_ENTREE_LIMITE_1-", "-DATE_ENTREE_LIMITE_2-")
            handled = True
    return handled

def handle_birth_date_filters():
    """ Searches users by [dateOfBirth], if [dateOfBirth] was given"""
    
    handled = False
    if validator.check_if_birth_date_was_given() is True :
            if validator.check_dropdown_options("-COMBO_FILTRE_1-") is True:
                data = filter_by_birth_date(window["-COMBO_FILTRE_1-"].get())
                window['-TABLE-'].update(values=data)
                clean_inputs("-DATE_NAISSANCE-", "-COMBO_FILTRE_1-")
                handled = True
    return handled

def get_search_mode(values):
    if values["-RADIO_SEARCH_EXACT-"] is True:
        return "complete_match"
    elif values["-RADIO_SEARCH_APPROXIMATIF-"] is True:
        return "pattern_match"

def handle_username_search(values):
    """ Searches users by [username], if [username] is valid"""

    search_mode = get_search_mode(values)
    username = values["-username_input-"]
    data = []
    if validator.check_username_validity() is True:
        if search_mode == "complete_match":
            data = db.get_by_username(username)
        elif search_mode == "pattern_match":
            data = db.get_by_username_pattern(username)
        window['-TABLE-'].update(values=data)
        clean_inputs("-username_input-")

def get_all_users_in_table():
    """ Get all users in db """
    data = db.get_all_service()
    window['-TABLE-'].update(values=data)
    window["-username_input-"].update("")
    
def clean_inputs(*keys):
    for key in keys:
        window[key].update("")

def get_current_table_data():
    tableData = window["-TABLE-"].get()
    return tableData

def get_data_sorted_by_username(data):
    return data[layout.tableColumns["Username"]]
def get_data_sorted_by_public_ip(data):
    return data[layout.tableColumns["Public IP"]]
def get_data_sorted_by_birthDate(data):
    return data[layout.tableColumns["Birth Date"]]
def get_data_sorted_by_entryDate(data):
    return data[layout.tableColumns["Entry Date"]]

def sort_table(column):
    data = get_current_table_data()
    if column in ("Username", "-tri_username-"):
        data.sort(key=get_data_sorted_by_username)
    elif column in ("Public IP", "-tri_public_ip-"):
        data.sort(key=get_data_sorted_by_public_ip)
    elif column in ("Birth Date", "-tri_date_naissance-"):
        data.sort(key=get_data_sorted_by_birthDate)
    elif column in ("Entry Date", "-tri_date_entree-"):
        data.sort(key=get_data_sorted_by_entryDate)
    window['-TABLE-'].update(values=data)

def handle_public_ip_search(values):
    """ Searches users by [public ip], if [public ip] is valid"""
    if validator.check_if_public_ip_was_given() is True:
        data = db.get_all_by_public_ip(values["-public_ip_input-"])
        window['-TABLE-'].update(values=data)
        clean_inputs("-public_ip_input-")


def save_table_data_in_csv_file():
    def get_saving_path():
        return window["-saving_path-"].get()
    
    if validator.check_saving_path_was_given() is True:
        dataToSave = get_current_table_data()
        savingPath = get_saving_path()
        with open(savingPath , "w", newline="") as file:
            writer = csv.writer(file)
            writer.writerow(dataToSave)

def popup_aborted():
    sg.popup_quick_message('Aborted', background_color='red', auto_close_duration=2)

def handle_sorting(event):
    """ Sort the table with the sorting button that has been clicked. """
    # Highlight selected button's background color
    for index in layout.sortingKeys:
            window[index].update(button_color=sg.theme_button_color())
    window[event].update(button_color=layout.selected_color)
    active_radio_button = event
    # Sorting
    sort_table(active_radio_button)
