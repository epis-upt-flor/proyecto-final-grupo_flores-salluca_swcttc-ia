from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import TimeoutException, NoSuchElementException
from selenium.webdriver.support.ui import Select
import time


def Registrarse(driver, url):
    # Definir tiempo de espera
    wait = WebDriverWait(driver, 60)

    # Agregar espera implícita
    driver.implicitly_wait(10)

    # Abrir la página web
    driver.get(url)

    try:
        # Esperar hasta que el enlace 'Registrate' esté presente
        enlace_registrarse = wait.until(EC.presence_of_element_located((By.XPATH, '//li/a[text()="Registrate" and @data-toggle="modal"]')))
        enlace_registrarse.click()

    except TimeoutException:
        print("Tiempo de espera agotado. No se pudo encontrar el elemento 'Registrate'.")
        driver.save_screenshot("captura_error_registro.png")
    except NoSuchElementException:
        print("No se pudo encontrar el elemento 'Registrate'.")
        driver.save_screenshot("captura_error_registro.png")


def completar_formulario_registrar(driver):
    # Después de hacer clic en 'Ingresar', esperar a que la página del formulario se cargue completamente
    form_wait = WebDriverWait(driver, 30)

    try:
        
        campo_usuario = form_wait.until(EC.presence_of_element_located((By.XPATH, '//input[@name="Nombre"]')))
        campo_usuario.send_keys("Tecnico66")

        campo_usuario = form_wait.until(EC.presence_of_element_located((By.XPATH, '//input[@name="Apellido"]')))
        campo_usuario.send_keys("Tecnico66")
        
        campo_usuario = form_wait.until(EC.presence_of_element_located((By.XPATH, '//input[@name="Correo"]')))
        campo_usuario.send_keys("Tecnico66@gmail.com")

        campo_contrasena = form_wait.until(EC.presence_of_element_located((By.XPATH, '//input[@name="Password"]')))
        campo_contrasena.send_keys("1234")

        # Localiza el elemento select
        select_element = form_wait.until(EC.presence_of_element_located((By.XPATH, '//select[@name="Id_Tipo_Usuario"]')))
        # Utiliza Select para interactuar con el elemento select
        select = Select(select_element)
        # Selecciona la opción con el valor "3"
        select.select_by_value("2")

        # Agregar un tiempo de espera de 10 segundos
        time.sleep(10)

        # Hacer clic en el botón de enviar el formulario
        boton_enviar = form_wait.until(EC.presence_of_element_located((By.XPATH, '//button[@id="btnEnviar" and @type="submit"]')))
        boton_enviar.click()

    except TimeoutException:
        print("Tiempo de espera agotado. No se pudo cargar completamente el formulario.")
        driver.save_screenshot("captura_error_formulario.png")
    except NoSuchElementException:
        print("No se pudo encontrar algún elemento del formulario.")
        driver.save_screenshot("captura_error_formulario.png")


# Especificar la ubicación del controlador
chrome_path = r"C:\Users\Lotus\Desktop\chromedriver-win64\chromedriver-win64\chromedriver.exe"

# Configurar el controlador del navegador
service = webdriver.chrome.service.Service(chrome_path)

# Crear una instancia de Chrome con el controlador configurado
driver = webdriver.Chrome(service=service)

url_principal = 'https://sistemaartemis.sytes.net/'

# Llamar a la función para iniciar sesión
Registrarse(driver, url_principal)

# Llamar a la función para completar el formulario
completar_formulario_registrar(driver)




# Agregar un tiempo de espera de 10 segundos
time.sleep(10)

# Cerrar el navegador al finalizar
driver.quit()




