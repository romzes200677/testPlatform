import React from 'react';
import PropTypes from 'prop-types';
import styles from './Pagination.module.css';

const Pagination = ({ currentPage, totalPages, onPageChange }) => {
  return (
      <div className={styles.paginationContainer}>
        <ul className={styles.pagination}>
          <li className={styles.paginationItem}>
            <button
                className={styles.paginationLink}
                disabled={currentPage === 1}
                onClick={() => onPageChange(currentPage - 1)}
            >
              Previous
            </button>
          </li>
          {Array.from({ length: totalPages }, (_, i) => i + 1).map((page) => (
              <li key={page} className={styles.paginationItem}>
                <button
                    className={`${styles.paginationLink} ${currentPage === page ? styles.active : ''}`}
                    onClick={() => onPageChange(page)}
                >
                  {page}
                </button>
              </li>
          ))}
          <li className={styles.paginationItem}>
            <button
                className={styles.paginationLink}
                disabled={currentPage === totalPages}
                onClick={() => onPageChange(currentPage + 1)}
            >
              Next
            </button>
          </li>
        </ul>
      </div>
  );
};

Pagination.propTypes = {
  currentPage: PropTypes.number.isRequired,
  totalPages: PropTypes.number.isRequired,
  onPageChange: PropTypes.func.isRequired,
};

export default Pagination;
