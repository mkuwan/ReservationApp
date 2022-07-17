import {Image, Navbar, NavbarBrand} from "react-bootstrap";


export const Header = () => {

    return(
        <>
            <Navbar fixed={'top'} className={'justify-content-center'}>
                <Navbar.Brand>
                    <Image/>
                </Navbar.Brand>
                <big>予約がいつも空いてるクリニック</big>
            </Navbar>
        </>
    )
}

export default Header;