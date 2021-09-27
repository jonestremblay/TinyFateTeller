import PySimpleGUI as sg
from . import database as db
from . import gui_layout as layout

window = layout.get_main_window()

def check_dropdown_options(key):
    """ Returns 'False' if no options was selected in the dropdown. """
    gave_option = False
    # if no options was picked
    if window[key].get() == "":
        sg.popup_ok("Please pick an option in the dropdown.", title="No options picked.")
        gave_option = False
    else:
        gave_option = True
    return gave_option


def check_if_birth_date_was_given():
    """ Returns 'False' if no dateOfBirth was given. """
    birth_date_was_given = False
    if window["-DATE_NAISSANCE-"].get() == "":
        sg.popup_ok("The date of birth is missing.", title="No birth date")
        birth_date_was_given = False
    else:
        birth_date_was_given = True
    return birth_date_was_given

def check_if_public_ip_was_given():
    """ Returns 'False' if no public_up was given. """

    public_ip_was_given = False
    if window["-public_ip_input-"].get() == "":
        sg.popup_ok("The public ip is missing.", title="No public IP")
        public_ip_was_given = False
    else:
        public_ip_was_given = True
    return public_ip_was_given

def check_if_entry_dates_were_given():
    """ Returns 'False' if entry date(s) are missing. """

    startingDateValid = True
    endingDateValid = True
    pop_up_msg = ""
    if window["-DATE_ENTREE_LIMITE_1-"].get() == "":
        startingDateValid = False
    if window["-DATE_ENTREE_LIMITE_2-"].get() == "":
        endingDateValid = False
        
    if not startingDateValid and not endingDateValid:
        pop_up_msg = "Please pick a starting and a ending entry date."
    elif not startingDateValid and endingDateValid:
        pop_up_msg = "Please pick the starting entry date"
    elif not endingDateValid and startingDateValid:
        pop_up_msg = "Please pick the ending entry date"
    if pop_up_msg != "":
        sg.popup_ok(pop_up_msg, title="")
        return False
    else:
        return True


def check_username_validity():
    """ Returns 'False' if no username was given. """

    print("Username input --> ", window["-username_input-"].get())
    if window["-username_input-"].get() == "":
        sg.popup_ok("No username has been entered.", title="Oops", keep_on_top=True, 
                    auto_close=True, auto_close_duration=5)
        return False
    else:
        return True


def check_saving_path_was_given():
    """ Returns 'False' if no saving_path was given. """

    if window["-saving_path-"].get() == "":
        sg.popup_ok("Please choose a saving path before trying to save.")
        return False
    else:
        return True

