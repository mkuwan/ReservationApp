import {Image, Navbar, NavbarBrand} from "react-bootstrap";


export const Header = () => {

    return(
        <>
            <Navbar fixed={'top'}>
                <Navbar.Brand>
                    <Image/>
                </Navbar.Brand>
                <big>予約がいつも空いてるクリニックへようこそ！！</big>
            </Navbar>
        </>
    )
}

export default Header;