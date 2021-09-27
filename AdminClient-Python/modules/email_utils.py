import PySimpleGUI as sg
from . import gui as gui
# used for sending the email
import smtplib  as smtp
# used to build the email
from email.message import EmailMessage
from os.path import basename
from email.mime.application import MIMEApplication
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
from email.utils import COMMASPACE, formatdate
from email import encoders
from email.mime.base import MIMEBase

DEFAULT_EMAIL = "webmailpython@gmail.com"
DEFAULT_EMAIL_USER = DEFAULT_EMAIL
DEFAULT_EMAIL_PASS = "withSMTP"

def define_smtp_server(user):
    google_smtp_server = 'smtp.gmail.com', 587
    microsoft_smtp_server = 'smtp.office365.com', 587
    yahoo_smtp_server = 'smtp.mail.yahoo.com', 587  # or port 465

    smtp_server = ""
    # open the email server connection
    if 'gmail' in user:
        smtp_host, smtp_port = google_smtp_server
        smtp_server = "gmail"
    elif 'hotmail' in user or 'live' in user or 'outlook' in user:
        smtp_host, smtp_port = microsoft_smtp_server
        smtp_server = "outlook"
    elif 'yahoo' in user:
        smtp_host, smtp_port = yahoo_smtp_server
        smtp_server = "yahoo"
    else:
        smtp_host, smtp_port = google_smtp_server
        smtp_server = "unknown"
    return smtp_host, smtp_port, smtp_server

def get_smtp_server_logo(smtp_server):
    image = ""
    if smtp_server == "gmail":
        image = ""
    elif smtp_server == "outlook":
        image = ""
    elif smtp_server == "yahoo":
        image = ""
    elif smtp_server == "unknown":
        image = ""

def send_an_email(sender_email, user, password, receiver_email, subject, body, files = None):
    """ Files need to be in a single string, separated with ' ; '    """
    host, port = define_smtp_server(user)[0], define_smtp_server(user)[1]
    server = smtp.SMTP(host=host, port=port)
    server.starttls()
    server.login(user=user, password=password)

    # create the email message headers and set the payload
    subject = "Users Data from Python"

    # Create a multipart message and set headers
    message = MIMEMultipart()
    message["From"] = sender_email
    message["To"] = receiver_email
    message["Subject"] = subject
    # message["Bcc"] = receiver_email  # Recommended for mass emails

    # Add body to email
    message.attach(MIMEText(body, "plain"))
    
    # Iterate through each optionnal attachement files
    for file in files.split(";"):
        # Open file in binary mode
        with open(file, "rb") as attachment:
            # Add file as application/octet-stream
            # Email client can usually download this automatically as attachment
            part = MIMEBase("application", "octet-stream")
            part.set_payload(attachment.read())

        # Encode file in ASCII characters to send by email    
        encoders.encode_base64(part)

        # Add header as key/value pair to attachment part
        part.add_header(
            "Content-Disposition",
            f"attachment; filename= {file}",
        )

        # Add attachment to message and convert message to string
        message.attach(part)

    text = message.as_string()

    # open the email server and send the mail
    server.sendmail(sender_email, receiver_email, text)

    server.close()

def launch_default_email_sender():
    mailServer = define_smtp_server(DEFAULT_EMAIL_USER)[2]
    emailWhereToSend = sg.popup_get_text("At what email address should we send the data ?", title="Step 1/4")
    if emailWhereToSend != None:
        subject = sg.popup_get_text("Please enter the email's subject.", title="Step 2/4", 
                                    icon="icons/" + mailServer + ".ico")
        if subject != None:
            filePath = sg.popup_get_file("Please choose one or multiple CSV files containing the data you want to send.", 
                                    title="Step 3/4", multiple_files=True, file_types=(("CSV files", "*.csv"),) )
            if filePath != None:
                body = sg.popup_get_text("Please enter the email's body content.", title="Step 4/4", 
                                        icon="icons/" + mailServer + ".ico")
                if body != None:
                    sg.popup_quick_message('Sending your message... this can take a moment...', background_color='green', auto_close_duration=5)
                    send_an_email(
                        sender_email = DEFAULT_EMAIL,
                        user = DEFAULT_EMAIL_USER,
                        password = DEFAULT_EMAIL_PASS,
                        receiver_email = emailWhereToSend,
                        subject = subject,
                        body = body,
                        files = filePath
                    )
                else:
                    gui.popup_aborted()
            else:
                gui.popup_aborted()
        else:
            gui.popup_aborted()
    else:
        gui.popup_aborted()



def launch_email_sender():
    senderEmail = sg.popup_get_text("What is the email you want to use for sending data ?", title="Step 1/7")
    if senderEmail != None:
        user = sg.popup_get_text("Please enter the username for that email account.", 
                                    title="Step 2/7", default_text=senderEmail)
        if user != None:
            mailServer = define_smtp_server(user)[2]
            password = sg.popup_get_text("Please enter the password for that email account.", 
                                            title="Step 3/7", password_char=True, icon="icons/" + mailServer + ".ico")
            if password != None:
                emailWhereToSend = sg.popup_get_text("Thank you ! At what email address should we send the data ?", 
                                                        title="Step 4/7")
                if emailWhereToSend != None:
                    subject = sg.popup_get_text("Please enter the email's subject.", title="Step 5/7", 
                                                    icon="icons/" + mailServer + ".ico")
                    if subject != None:
                        filePath = sg.popup_get_file("Please choose one or multiple CSV files containing the data" + 
                                                        "you want to send.", title="Step 6/7", multiple_files=True,
                                                        file_types=(("CSV files", "*.csv"),), 
                                                        icon="icons/" + mailServer + ".ico" )
                        if filePath != None:
                            body = sg.popup_get_text("Please enter the email's body content.", title="Step 7/7", 
                                                        icon="icons/" + mailServer + ".ico")
                            if body != None:
                                sg.popup_quick_message('Sending your message... this can take a moment...', 
                                                        background_color='green', auto_close_duration=5)
                                # Send email
                                send_an_email(
                                    sender_email = senderEmail,
                                    user = user,
                                    password = password,
                                    receiver_email = emailWhereToSend,
                                    subject = subject,
                                    body = body,
                                    files = filePath
                                )
                            else:
                                gui.popup_aborted()
                        else:
                            gui.popup_aborted()
                else:
                    gui.popup_aborted()
            else:
                gui.popup_aborted()
        else:
            gui.popup_aborted()
    else:
        gui.popup_aborted()
