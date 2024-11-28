import re
from playwright.sync_api import Playwright, sync_playwright, expect


def run(playwright: Playwright) -> None:
    browser = playwright.chromium.launch(headless=False)
    context = browser.new_context()
    page = context.new_page()
    # Abrir la página de login
    page.goto("http://localhost:5167/login")
    page.wait_for_selector("text=Email")
    page.wait_for_timeout(4000)
    page.screenshot(path="screenshots/1_login_page.png")

    # Intentar iniciar sesión con datos vacíos
    page.get_by_label("Email").click()
    page.get_by_role("button", name="Login").click()
    page.wait_for_timeout(2000)
    page.screenshot(path="screenshots/2_empty_login_attempt.png")

    # Completar con datos inválidos
    page.get_by_label("Email").click()
    page.get_by_label("Email").fill("dasd")
    page.get_by_label("Password").click()
    page.get_by_label("Email").click(click_count=3)
    page.get_by_label("Email").fill("danas@gmail.com")
    page.get_by_label("Password").click()
    page.get_by_label("Password").fill("1213")
    page.get_by_role("button", name="Login").click()
    page.wait_for_timeout(2000)
    page.screenshot(path="screenshots/3_invalid_login_attempt.png")

    # Interacción con el modal de error
    page.get_by_role("button", name="OK").click()
    page.get_by_label("Email").click()
    page.get_by_label("Email").press("ControlOrMeta+Shift+ArrowLeft")
    page.get_by_label("Email").fill("jc-garcia@proton.me")
    page.get_by_label("Password").click()
    page.get_by_label("Password").fill("ddd")
    page.get_by_role("button", name="Login").click()
    page.wait_for_timeout(4000)
    page.screenshot(path="screenshots/4_second_invalid_login_attempt.png")

    # Login exitoso
    page.get_by_role("button", name="OK").click()
    page.get_by_label("Password").click()
    page.get_by_label("Password").fill("1234")
    page.get_by_role("button", name="Login").click()
    page.wait_for_selector("text=Ver Reservas")
    page.wait_for_timeout(4000)
    page.screenshot(path="screenshots/5_successful_login.png")

    # Navegar a reservas
    page.get_by_role("link", name="Ver Reservas").click()
    page.wait_for_selector("table")
    page.wait_for_timeout(4000)
    page.screenshot(path="screenshots/6_reservations_page.png")

    # Volver al inicio
    page.get_by_role("link", name="Volver").click()
    page.wait_for_timeout(4000)
    page.screenshot(path="screenshots/7_return_to_home.png")

    # Interacción con las filas
    page.get_by_role("row", name="My favourite fairy tales Tony").get_by_role("button").click()
    page.get_by_role("button", name="OK").click()
    page.wait_for_timeout(4000)
    page.screenshot(path="screenshots/8_fairy_tales_action.png")

    page.get_by_role("row", name="El señor de los anillos (").get_by_role("button").click()
    page.get_by_role("button", name="OK").click()
    page.wait_for_timeout(4000)
    page.screenshot(path="screenshots/9_lord_of_the_rings_action.png")

    # Buscar por título
    page.get_by_placeholder("Buscar por Título").click()
    page.get_by_placeholder("Buscar por Título").fill("El señor de los anillos")
    page.get_by_placeholder("Buscar por Título").press("Enter")
    page.wait_for_timeout(4000)
    page.screenshot(path="screenshots/10_search_results.png")

    # Navegar a notificaciones
    page.get_by_role("link", name="Notificaciones Notificaciones").click()
    page.wait_for_timeout(4000)
    page.screenshot(path="screenshots/11_notifications_page.png")

    # Volver al inicio desde notificaciones
    page.get_by_role("link", name="Volver").click()
    page.wait_for_timeout(4000)
    page.screenshot(path="screenshots/12_return_to_home_from_notifications.png")

    # ---------------------
    context.close()
    browser.close()


with sync_playwright() as playwright:
    run(playwright)
