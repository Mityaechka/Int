import { ApiModel } from './../model/api.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private path = 'https://localhost:44346/api';
  //private path = 'https://192.168.0.220:450/api';
  constructor(private http: HttpClient) {}
  public get<T>(url: string): Promise<ApiModel<T>> {
    const promise = new Promise<ApiModel<T>>((resolve, reject) => {
      const req = this.http
        .get(`${this.path}/${url}`, { withCredentials: true })
        .toPromise();
      req.then(
        (data: any) => {
          resolve(data);
        },
        (error) => {
          resolve(new ApiModel(false, 'Ошибка сервера', null));
        }
      );
    });
    return promise;
  }
  public post<T>(
    url: string,
    content: any,
    options?: { isJson }
  ): Promise<ApiModel<T>> {
    const o = Object.assign({ isJson: true }, options ?? {});
    let data;
    let headers = new HttpHeaders();
    if (o.isJson) {
      data = JSON.stringify(content);
      headers = headers.append('content-type', 'application/json');
    } else {
      data = content;
    }
    const promise = new Promise<ApiModel<T>>((resolve, reject) => {
      const req = this.http
        .post(`${this.path}/${url}`, data, {
          withCredentials: true,
          headers,
        })
        .toPromise();
      req.then(
        (data: any) => {
          resolve(data);
        },
        (error) => {
          resolve(new ApiModel(false, 'Ошибка сервера', null));
        }
      );
    });
    return promise;
  }
  public postForm<T>(url: string, content: any): Promise<ApiModel<T>> {
    const data = this.jsonToFormData(content);
    return this.post<T>(url, data, { isJson: false });
  }
  private buildFormData(formData, data, parentKey?) {
    if (
      data &&
      typeof data === 'object' &&
      !(data instanceof Date) &&
      !(data instanceof File)
    ) {
      Object.keys(data).forEach((key) => {
        const c = isNaN(Number(key));
        this.buildFormData(
          formData,
          data[key],
          parentKey
            ? !isNaN(Number(key))
              ? `${parentKey}[${key}]`
              : `${parentKey}.${key}`
            : key
        );
      });
    } else {
      const value = data == null ? '' : data;

      formData.append(parentKey, value);
    }
  }

  private jsonToFormData(data) {
    const formData = new FormData();

    this.buildFormData(formData, data);

    return formData;
  }
}
