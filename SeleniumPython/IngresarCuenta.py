from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import TimeoutException, NoSuchElementException
import time

def iniciar_sesion(driver, url):
    # Definir tiempo de espera
    wait = WebDriverWait(driver, 60)

    # Agregar espera implícita
    driver.implicitly_wait(10)

    # Abrir la página web
    driver.get(url)

    try:
        # Esperar hasta que el enlace 'Ingresar' esté presente
        enlace_ingresar = wait.until(EC.presence_of_element_located((By.XPATH, '//li/a[@href="/Login/Index"]')))
        enlace_ingresar.click()

    except TimeoutException:
        print("Tiempo de espera agotado. No se pudo encontrar el elemento 'Ingresar'.")
        driver.save_screenshot("captura_error_ingreso.png")
    except NoSuchElementException:
        print("No se pudo encontrar el elemento 'Ingresar'.")
        driver.save_screenshot("captura_error_ingreso.png")


def completar_formulario(driver):
    # Después de hacer clic en 'Ingresar', esperar a que la página del formulario se cargue completamente
    form_wait = WebDriverWait(driver, 30)

    try:
        # Esperar hasta que un campo de entrada del formulario esté presente
        campo_usuario = form_wait.until(EC.presence_of_element_located((By.XPATH, '//input[@name="Correo"]')))
        campo_usuario.send_keys("Cliente66@gmail.com")
        #campo_usuario.send_keys("Tecnico36@gmail.com")

        # Esperar hasta que el campo de contraseña esté presente
        campo_contrasena = form_wait.until(EC.presence_of_element_located((By.XPATH, '//input[@name="Password"]')))
        campo_contrasena.send_keys("1234")


        # Hacer clic en el botón de enviar el formulario
        boton_enviar = form_wait.until(EC.presence_of_element_located((By.XPATH, '//button[text()="Iniciar Sesión" and @type="submit"]')))
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
iniciar_sesion(driver, url_principal)

# Llamar a la función para completar el formulario
completar_formulario(driver)



# Agregar un tiempo de espera de 10 segundos
time.sleep(10)

# Cerrar el navegador al finalizar
driver.quit()
