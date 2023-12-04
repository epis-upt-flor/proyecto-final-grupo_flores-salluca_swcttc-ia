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
        campo_usuario.send_keys("Tecnico66@gmail.com")
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


def completar_postular(driver):
    # Después de hacer clic en 'Ingresar', esperar a que la página del formulario se cargue completamente
    form_wait = WebDriverWait(driver, 30)
    # Definir tiempo de espera
    wait = WebDriverWait(driver, 30)

    #Despleguar barra lateral
    # Esperar hasta que el enlace 'Ingresar' esté presente    
    enlace_mis_servicios = wait.until(EC.presence_of_element_located((By.XPATH, '//li[@class="nav-item"]//a[@data-target="#collapseAdministracion"]')))
    enlace_mis_servicios.click()
    # Esperar hasta que el enlace 'Ingresar' esté presente
    enlace_publicar_servicio = wait.until(EC.presence_of_element_located((By.XPATH, '//div[@id="collapseAdministracion"]//a[@class="collapse-item" and @href="/Servicio/List"]')))
    enlace_publicar_servicio.click()

    #Realiza click en postular
    # Localiza el enlace "Postular"
    #enlace_postular = wait.until(EC.presence_of_element_located((By.XPATH, '//a[@href="/Archivo/Subir/4" and @class="btn btn-success" and contains(@title, "Postular")]')))
    enlace_postular = wait.until(EC.presence_of_element_located((By.XPATH, '//a[@class="btn btn-success" and contains(@title, "Postular")]')))
    enlace_postular.click()

    # Localiza el campo de entrada de texto por el nombre
    campo_nombre_archivo = form_wait.until(EC.presence_of_element_located((By.XPATH, '//input[@name="Nombre" and @type="text"]')))
    campo_nombre_archivo.send_keys("Nombre_del_Archivo")


    # Localiza el botón "Subir Archivo"
    #boton_subir_archivo = form_wait.until(EC.presence_of_element_located((By.XPATH, '//button[@class="btn btn-success" and contains(text(), "Subir Archivo")]')))
    #boton_subir_archivo.click()

    # Localiza el elemento de entrada de tipo "file"
    #campo_archivo = form_wait.until(EC.presence_of_element_located((By.XPATH, '//input[@name="Archivo" and @type="file" and @id="Documento"]')))
    # Aquí puedes adjuntar el archivo deseado
    #archivo_path = r"C:\Users\Lotus\Downloads\Articulos\g03_r-gcsw007.doc"
    #Cambia la ruta a la ubicación de tu archivo
    #campo_archivo.send_keys(archivo_path)

    # Agregar un tiempo de espera de 10 segundos
    time.sleep(10)

    # Localiza el botón "Subir Archivo"
    boton_subir_archivo = form_wait.until(EC.presence_of_element_located((By.XPATH, '//button[@class="btn btn-success" and contains(text(), "Subir Archivo")]')))
    boton_subir_archivo.click()

    # Localiza el botón "Confirmar"
    boton_confirmar = form_wait.until(EC.presence_of_element_located((By.XPATH, '//input[@type="submit" and @value="Confirmar"]')))
    boton_confirmar.click()




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

completar_postular(driver)


# Agregar un tiempo de espera de 10 segundos
time.sleep(10)

# Cerrar el navegador al finalizar
driver.quit()
