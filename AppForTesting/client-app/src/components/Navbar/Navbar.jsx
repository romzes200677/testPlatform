// components/Navbar/Navbar.jsx
import styles from './Navbar.module.css';
import { Link } from 'react-router-dom';

const Navbar = () => {
  return (
    <nav className={styles.navbar}>
      <div className={styles.logo}>
        <Link to="/">Stepik</Link>
      </div>
      
      <div className={styles.breadcrumbs}>
        <Link to="/">Главная</Link> / <span>Тестирование</span>
      </div>
      
      <div className={styles.userSection}>
        <img 
          src="https://via.placeholder.com/40" 
          alt="User" 
          className={styles.avatar}
        />
      </div>
    </nav>
  );
};

export default Navbar;