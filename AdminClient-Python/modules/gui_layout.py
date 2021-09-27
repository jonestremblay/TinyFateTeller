from logging import disable
from PIL import Image, ImageTk, ImageSequence
import PySimpleGUI as sg
from PySimpleGUI.PySimpleGUI import Column
from win32api import GetSystemMetrics
from . import database as db

sg.theme('DarkPurple1')
# sg.theme('DarkPurple1')

optionsFiltre1 = ["exactement", "avant", "après"]
optionsFiltre2 = ["Aujourd'hui", "Les derniers 7 jours", "Ce mois-ci", "3 derniers mois", "6 derniers mois", "Dernière année"]
optionsTri = ["date d'entrée", "date de naissance", "username", "public IP"]
sortingKeys = ["-tri_date_entree-", "-tri_date_naissance-", "-tri_username-", "-tri_public_ip-"]
filtre1_frame = [
    [sg.Text("Né"), sg.Combo(values=optionsFiltre1, default_value=None, readonly=True, k='-COMBO_FILTRE_1-'),
     sg.Text("le"), sg.CalendarButton('[ choisir date ]', close_when_date_chosen=False,
                                      target="-DATE_NAISSANCE-", format='%Y-%m-%d T%H:%M:%S', default_date_m_d_y=(2,12,1998),
                                      no_titlebar=False, title="Birth Date", key="-BIRTH_DATE_INPUT-"),
     sg.Text("", key='-DATE_NAISSANCE-', size=(8,1))],
    [sg.Button("Filtrer par date de naissance", key="-btn_filtre_birthDate-")]
]

colDatePicker = [
    [sg.CalendarButton('[ choisir date ]', close_when_date_chosen=False,
                       target="-DATE_ENTREE_LIMITE_1-", no_titlebar=False, format='%Y-%m-%d T%H:%M:%S', title="Date entrée", key="-DATE_ENTREE_1-"), ],
     [sg.Text("et le")],
     [sg.CalendarButton('[ choisir date ]', close_when_date_chosen=False, target="-DATE_ENTREE_LIMITE_2-",
                       no_titlebar=False, format='%Y-%m-%d T%H:%M:%S', title="Date entrée 2", key="DATE_ENTREE_2")]
]

colDateValues = [
   
        [sg.Text("", size=(8,1), key='-DATE_ENTREE_LIMITE_1-')],
        [sg.Text("", size=(8,2))],
        [sg.Text("", size=(8,1), key='-DATE_ENTREE_LIMITE_2-')]
]

filtre2_frame = [
    [sg.Radio('', "RadioDemo", default=True, size=(10, 1), k='-RADIO_DROPLIST-', enable_events=True),
     sg.Combo(values=optionsFiltre2, default_value=None, readonly=True, k='-COMBO_FILTRE_2-'), ],
    [sg.Radio('Entre le ', "RadioDemo", default=False, size=(10, 1), k='-RADIO_CALENDAR-', enable_events=True),
     sg.Column(colDatePicker),
     sg.Column(colDateValues)],
    [sg.Button("Filtrer par date d'entrée", key="-btn_filtre_entryDate-")]
]


filtres_frame = [
    [sg.Frame("... par date de naissance", filtre1_frame, font="Any 12 bold", title_color='white')],
    [sg.Frame("... par date d'entrée", filtre2_frame, font="Any 12 bold", title_color='white')]
]

triColumn_1 = [
    [sg.B(optionsTri[0], key=sortingKeys[0]), sg.B(optionsTri[2], key=sortingKeys[2])]
]

triColumn_2 = [
    [sg.B(optionsTri[1], key=sortingKeys[1]), sg.B(optionsTri[3], key=sortingKeys[3])]
]

tri_frame = [
    [sg.Column(triColumn_1)],
    [sg.Column(triColumn_2)]
]

search_frame = [
    [sg.In("", key="-username_input-", enable_events=True)],
    [sg.Radio('', "search_mode_radios", default=True, k='-RADIO_SEARCH_EXACT-', enable_events=True),
        sg.Text("Match complet",   key='-text_search_mode_exact-'), 
     sg.Radio('', "search_mode_radios", default=False,  k='-RADIO_SEARCH_APPROXIMATIF-', enable_events=True),
        sg.Text("Match approximatif",  key='-text_search_approximatif-') ],
    [sg.Button("Recherche par username", key="-btn_search_username-")]
]

search_ip_frame = [
    [sg.In("", key="-public_ip_input-", enable_events=True)],
    [sg.Button("Recherche par IP publique", key="-btn_search_public_ip-")]
]

search_sort_frame = [
    [sg.Frame("Rechercher par username", search_frame, font="Any 12 bold", title_color="white",
              element_justification="c")],
    [sg.Frame("Rechercher par IP publique", search_ip_frame, font="Any 12 bold", title_color="white",
              element_justification="c")], 
    [sg.Frame("Options de tri", tri_frame, font="Any 12 bold", title_color="white", element_justification="c")]
]

columnFiltre = [
    [sg.Frame("Filtrer", filtres_frame, font="Any 12 bold", title_color='white', key="-filtre_frame-")],
]

options_frame = [
    [sg.Column(search_sort_frame, element_justification="c"), sg.Column(columnFiltre, element_justification="c")],
    [ sg.Text("", size=(70,1)), sg.Button("Voir tous les utilisateurs", key="-btn_getAll-")]
]

tableHeadings = ["Entry Date", "Hostname", "Local IP", "Public IP", "Username", "Birth Date"]
# data = """ [
   # ["yyyy-mm-dd", "DESKTOP-JONES", "192.168.2.1", "78.885.22.24", "jones", "2019-08-12"]
# ] """

tableColumns = {
    "Entry Date": 0,
    "Hostname" : 1,
    "Local IP" : 2,
    "Public IP" : 3,
    "Username" : 4,
    "Birth Date" : 5
}

# ------ Menu Definition ------ #
menu_def = [
    ['&Envoyer les résultats', ['Par courriel avec ...', ['... le email par défaut', '... mon email personnel', ], '---', '!Par texto'] ]
]

data = db.get_all_service()
layout = [
    [sg.Menu(menu_def)],
    [sg.Frame("Options", options_frame, font="Any 12 bold", title_color="white", element_justification="c", vertical_alignment="c")],
    [sg.Table(values=data, headings=tableHeadings, max_col_width=25,
              auto_size_columns=True,
              display_row_numbers=True,
              justification='right',
              num_rows=8,
              alternating_row_color='#446674',
              key='-TABLE-',
              row_height=35,
              tooltip='This is a table')],
    [sg.Button("Exit"), sg.SaveAs("Save as...", k="-btn_save_as-", target="-saving_path-", file_types=(("CSV files", "*.csv"),)), 
        sg.Text("Path where to save as CSV: "), sg.Input(key="-saving_path-", size=(35,1), disabled=True, text_color='black'),
        sg.Button("Save", k="-btn_save-")]
]

screenWidth =  GetSystemMetrics(0)
screenHeigth =  GetSystemMetrics(1)

splashGIF = r'gifs/splashGIf.gif'
splash_layout = [[sg.Image(key='-IMAGE-')]]
splashWindow = sg.Window('Window Title', splash_layout, margins=(0,0), 
                    element_padding=(0,0), no_titlebar=True, finalize=True, keep_on_top=True, auto_close=True, 
                    auto_close_duration=3, location=(screenWidth/2 - 200, screenHeigth/2 - 200))

sequence = [ImageTk.PhotoImage(img) for img in ImageSequence.Iterator(Image.open(splashGIF))]
interframe_duration = Image.open(splashGIF).info['duration']

window = sg.Window('Admin of API Panel Draft', layout, element_justification='c', finalize=True)

selected_color = ('red', 'white')

def get_splash_window():
    return splashWindow

def get_main_window():
    return window
