// components/Sidebar/Sidebar.jsx
import styles from './Sidebar.module.css';
import { NavLink } from 'react-router-dom';

const Sidebar = () => {
  const courses = [
    { id: 'math', name: 'Математика' },
    { id: 'physics', name: 'Физика' },
    { id: 'programming', name: 'Программирование' },
  ];

  return (
    <aside className={styles.sidebar}>
      <h3 className={styles.sidebarTitle}>Курсы</h3>
      <ul className={styles.courseList}>
        {courses.map(course => (
          <li key={course.id}>
            <NavLink 
              to={`/course/${course.id}`}
              className={({ isActive }) => 
                isActive ? styles.activeCourse : styles.courseItem
              }
            >
              {course.name}
            </NavLink>
          </li>
        ))}
      </ul>
    </aside>
  );
};

export default Sidebar;