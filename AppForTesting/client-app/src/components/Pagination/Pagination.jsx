import React from 'react';
import styles from './Pagination.module.css';

const Pagination = ({ totalPages, currentPage, paginate }) => {
  return (
    <div className={styles.pagination}>
      {Array.from({ length: totalPages }, (_, i) => i + 1).map((page) => (
        <button
          key={page}
          className={`${styles.pageButton} ${currentPage === page ? styles.active : ''}`}
          onClick={() => paginate(page)}
        >
          {page}
        </button>
      ))}
    </div>
  );
};

export default Pagination;
